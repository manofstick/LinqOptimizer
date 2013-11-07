﻿namespace LinqOptimizer.Core
    
    open System
    open System.Collections
    open System.Collections.Generic
    open System.Linq
    open System.Linq.Expressions
    open System.Reflection
    open System.Collections.Concurrent

    // C#/Linq call patterns
    // TODO: expr type checks
    module internal CSharpExpressionOptimizer =

        let rec toQueryExpr (expr : Expression) : QueryExpr =
            match expr with
            | MethodCall (_, MethodName "Select" _, [expr'; Lambda ([_], bodyExpr) as f']) -> 
                Transform (optimize f' :?> LambdaExpression, toQueryExpr expr', bodyExpr.Type)
    
            | MethodCall (_, MethodName "Select" _, [expr'; Lambda ([_; _], bodyExpr) as f']) -> 
                TransformIndexed (optimize f' :?> LambdaExpression, toQueryExpr expr', bodyExpr.Type)
    
            | MethodCall (_, MethodName "Where" _, [expr'; Lambda ([paramExpr], _) as f']) -> 
                Filter (optimize f' :?> LambdaExpression, toQueryExpr expr', paramExpr.Type)
    
            | MethodCall (_, MethodName "Where" _, [expr'; Lambda ([paramExpr; indexExpr], _) as f']) -> 
                FilterIndexed (optimize f' :?> LambdaExpression, toQueryExpr expr', paramExpr.Type)
    
            | MethodCall (_, MethodName "Take" _, [expr'; countExpr]) when countExpr.Type = typeof<int> -> 
                let queryExpr = toQueryExpr expr'
                Take (optimize countExpr, queryExpr , queryExpr.Type)
    
            | MethodCall (_, MethodName "Skip" _, [expr'; countExpr]) when countExpr.Type = typeof<int> -> 
                let queryExpr = toQueryExpr expr'
                Skip (countExpr, queryExpr, queryExpr.Type)
    
            | MethodCall (_, (MethodName "SelectMany" [|_; _|] as m), [expr'; Lambda ([paramExpr], bodyExpr)]) -> 
                NestedQuery ((paramExpr, toQueryExpr bodyExpr), toQueryExpr expr', m.ReturnType.GetGenericArguments().[0])
    
            | MethodCall (_, MethodName "GroupBy" _, [expr'; Lambda ([paramExpr], bodyExpr) as f']) -> 
                GroupBy (optimize f' :?> LambdaExpression, toQueryExpr expr', typedefof<IGrouping<_, _>>.MakeGenericType [|paramExpr.Type; bodyExpr.Type|])
    
            | MethodCall (_, MethodName "OrderBy" _, [expr'; Lambda ([paramExpr], bodyExpr) as f']) -> 
                OrderBy (optimize f' :?> LambdaExpression, Order.Ascending, toQueryExpr expr', paramExpr.Type)
    
            | MethodCall (_, MethodName "Count" _,  [expr']) -> 
                let query' = toQueryExpr expr'
                Count (query', query'.Type)
    
            | MethodCall (_, MethodName "Range" _, [startExpr; countExpr]) ->
                RangeGenerator(optimize startExpr, optimize countExpr)

            | MethodCall (_, MethodName "Sum" _,  [expr']) -> 
                let query' = toQueryExpr expr'
                Sum(query', query'.Type)

            | NotNull expr -> 
                if expr.Type.IsArray then
                    Source (expr, expr.Type.GetElementType())
                else
                    Source (expr, expr.Type.GetGenericArguments().[0])
            | _ ->
                invalidArg "expr" "Cannot extract QueryExpr from null Expression"

        and private transformer (expr : Expression) : Expression option =
            match expr with
            | MethodCall (_, MethodName "Select" _,             [_; Lambda _ ]) 
            | MethodCall (_, MethodName "Select" _,             [_; Lambda _ ]) 
            | MethodCall (_, MethodName "Where" _,              [_; Lambda _ ])
            | MethodCall (_, MethodName "Where" _,              [_; Lambda _ ]) 
            | MethodCall (_, MethodName "Take" _,               [_; _        ])
            | MethodCall (_, MethodName "Skip" _,               [_; _        ])
            | MethodCall (_, MethodName "SelectMany" [|_; _|],  [_; Lambda _ ])
            | MethodCall (_, MethodName "GroupBy" _,            [_; Lambda _ ])
            | MethodCall (_, MethodName "OrderBy" _,            [_; Lambda _ ])
            | MethodCall (_, MethodName "Count" _,              [_           ])
            | MethodCall (_, MethodName "Range" _,              [_; _        ]) 
            | MethodCall (_, MethodName "Sum" _,                [_           ]) ->
                let query = toQueryExpr expr
                (Compiler.compileToSequential >> Some) query
            | _ ->
                None

        and private opt = ExpressionTransformer.transform transformer
        
        and optimize (expr : Expression) : Expression = opt expr
