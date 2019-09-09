using BenchmarkDotNet.Attributes;
using Nessos.LinqOptimizer.CSharp;
using Cistern.Linq;

namespace LinqOptimizer.Benchmarks.CSharp
{
    public partial class SequentialBenchmarks
    {
        /*
        |        Method |   Count |            Mean |          Error |         StdDev |    Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
        |-------------- |-------- |----------------:|---------------:|---------------:|---------:|--------:|-------:|------:|------:|----------:|
        | 'Sum Cistern' |       0 |        27.28 ns |      0.1769 ns |      0.1568 ns |     1.93 |    0.03 | 0.0102 |     - |     - |      32 B |
        |    'Sum Linq' |       0 |        14.10 ns |      0.1832 ns |      0.1624 ns |     1.00 |    0.00 |      - |     - |     - |         - |
        |     'Sum Opt' |       0 |    46,790.91 ns |    497.3814 ns |    465.2509 ns | 3,314.35 |   60.80 | 5.1880 |     - |     - |   16508 B |
        |               |         |                 |                |                |          |         |        |       |       |           |
        | 'Sum Cistern' |      10 |        42.05 ns |      0.2423 ns |      0.2266 ns |     0.55 |    0.01 | 0.0101 |     - |     - |      32 B |
        |    'Sum Linq' |      10 |        76.30 ns |      0.7380 ns |      0.6903 ns |     1.00 |    0.00 | 0.0101 |     - |     - |      32 B |
        |     'Sum Opt' |      10 |    47,269.71 ns |    339.5650 ns |    317.6293 ns |   619.60 |    7.57 | 5.1880 |     - |     - |   16508 B |
        |               |         |                 |                |                |          |         |        |       |       |           |
        | 'Sum Cistern' |     100 |       166.20 ns |      1.2624 ns |      1.1809 ns |     0.28 |    0.00 | 0.0100 |     - |     - |      32 B |
        |    'Sum Linq' |     100 |       604.18 ns |      3.6638 ns |      3.4271 ns |     1.00 |    0.00 | 0.0095 |     - |     - |      32 B |
        |     'Sum Opt' |     100 |    47,431.70 ns |    302.0929 ns |    282.5779 ns |    78.51 |    0.62 | 5.1880 |     - |     - |   16508 B |
        |               |         |                 |                |                |          |         |        |       |       |           |
        | 'Sum Cistern' |   10000 |    13,764.76 ns |     86.0308 ns |     80.4733 ns |     0.25 |    0.00 |      - |     - |     - |      32 B |
        |    'Sum Linq' |   10000 |    55,296.67 ns |    373.1092 ns |    330.7516 ns |     1.00 |    0.00 |      - |     - |     - |      32 B |
        |     'Sum Opt' |   10000 |    62,573.14 ns |    653.2285 ns |    611.0303 ns |     1.13 |    0.01 | 5.1270 |     - |     - |   16508 B |
        |               |         |                 |                |                |          |         |        |       |       |           |
        | 'Sum Cistern' | 1000000 | 1,389,519.47 ns |  7,837.1676 ns |  7,330.8914 ns |     0.25 |    0.00 |      - |     - |     - |      32 B |
        |    'Sum Linq' | 1000000 | 5,507,463.90 ns | 20,544.6872 ns | 18,212.3301 ns |     1.00 |    0.00 |      - |     - |     - |      32 B |
        |     'Sum Opt' | 1000000 | 1,437,876.07 ns | 11,039.3320 ns | 10,326.1980 ns |     0.26 |    0.00 | 3.9063 |     - |     - |   16509 B |
        */
        public partial class SumBechmarks : BenchmarkBase
        {
            [Benchmark(Description = "Sum Cistern")]
            public double SumCistern()
            {
                return values.Sum();
            }
        }

        /*
        |                   Method |   Count |            Mean |          Error |         StdDev |    Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
        |------------------------- |-------- |----------------:|---------------:|---------------:|---------:|--------:|-------:|------:|------:|----------:|
        | 'Sum of Squares Cistern' |       0 |        32.80 ns |      0.4647 ns |      0.4120 ns |     0.79 |    0.02 | 0.0101 |     - |     - |      32 B |
        |    'Sum of Squares Linq' |       0 |        41.33 ns |      0.8723 ns |      1.0046 ns |     1.00 |    0.00 |      - |     - |     - |         - |
        |     'Sum of Squares Opt' |       0 |    60,655.93 ns |  1,004.6357 ns |    939.7369 ns | 1,462.69 |   44.13 | 5.9204 |     - |     - |   18733 B |
        |                          |         |                 |                |                |          |         |        |       |       |           |
        | 'Sum of Squares Cistern' |      10 |       109.62 ns |      0.7830 ns |      0.7324 ns |     0.64 |    0.01 | 0.0483 |     - |     - |     152 B |
        |    'Sum of Squares Linq' |      10 |       172.53 ns |      1.4346 ns |      1.3419 ns |     1.00 |    0.00 | 0.0150 |     - |     - |      48 B |
        |     'Sum of Squares Opt' |      10 |    60,081.55 ns |    610.3621 ns |    570.9330 ns |   348.26 |    4.22 | 5.9204 |     - |     - |   18733 B |
        |                          |         |                 |                |                |          |         |        |       |       |           |
        | 'Sum of Squares Cistern' |     100 |       308.26 ns |      2.3056 ns |      2.1567 ns |     0.32 |    0.00 | 0.0482 |     - |     - |     152 B |
        |    'Sum of Squares Linq' |     100 |       952.33 ns |     10.2225 ns |      9.5621 ns |     1.00 |    0.00 | 0.0143 |     - |     - |      48 B |
        |     'Sum of Squares Opt' |     100 |    60,050.67 ns |    361.5115 ns |    338.1580 ns |    63.06 |    0.68 | 5.9204 |     - |     - |   18733 B |
        |                          |         |                 |                |                |          |         |        |       |       |           |
        | 'Sum of Squares Cistern' |   10000 |    20,931.97 ns |    108.3494 ns |    101.3501 ns |     0.24 |    0.00 | 0.0305 |     - |     - |     152 B |
        |    'Sum of Squares Linq' |   10000 |    88,243.58 ns |    793.2973 ns |    742.0508 ns |     1.00 |    0.00 |      - |     - |     - |      48 B |
        |     'Sum of Squares Opt' |   10000 |    76,492.69 ns |  1,187.3593 ns |  1,110.6567 ns |     0.87 |    0.01 | 5.8594 |     - |     - |   18733 B |
        |                          |         |                 |                |                |          |         |        |       |       |           |
        | 'Sum of Squares Cistern' | 1000000 | 2,078,028.80 ns | 16,876.6655 ns | 15,786.4434 ns |     0.24 |    0.00 |      - |     - |     - |     152 B |
        |    'Sum of Squares Linq' | 1000000 | 8,754,710.42 ns | 66,414.0752 ns | 62,123.7671 ns |     1.00 |    0.00 |      - |     - |     - |      48 B |
        |     'Sum of Squares Opt' | 1000000 | 1,457,266.62 ns | 12,679.7592 ns | 11,240.2762 ns |     0.17 |    0.00 | 5.8594 |     - |     - |   18735 B |
        */
        public partial class SumOfSquaresBechmarks : BenchmarkBase
        {
            [Benchmark(Description = "Sum of Squares Cistern")]
            public double SumSqCistern()
            {
                return values.Select(x => x * x).Sum();
            }
        }

        /*
        |              Method |   Count |             Mean |           Error |          StdDev |    Ratio | RatioSD |     Gen 0 | Gen 1 | Gen 2 | Allocated |
        |-------------------- |-------- |-----------------:|----------------:|----------------:|---------:|--------:|----------:|------:|------:|----------:|
        | 'Cartesian Cistern' |       0 |        107.11 ns |       0.3919 ns |       0.3665 ns |     1.51 |    0.01 |    0.0685 |     - |     - |     216 B |
        |    'Cartesian Linq' |       0 |         71.01 ns |       0.6543 ns |       0.5800 ns |     1.00 |    0.00 |    0.0533 |     - |     - |     168 B |
        |     'Cartesian Opt' |       0 |    109,861.61 ns |     481.1207 ns |     450.0406 ns | 1,547.06 |   12.58 |   11.1084 |     - |     - |   35050 B |
        |                     |         |                  |                 |                 |          |         |           |       |       |           |
        | 'Cartesian Cistern' |      10 |        199.06 ns |       1.2596 ns |       1.1166 ns |     0.71 |    0.01 |    0.0913 |     - |     - |     288 B |
        |    'Cartesian Linq' |      10 |        282.24 ns |       2.9057 ns |       2.7180 ns |     1.00 |    0.00 |    0.0734 |     - |     - |     232 B |
        |     'Cartesian Opt' |      10 |    109,093.80 ns |     652.4443 ns |     610.2968 ns |   386.55 |    3.88 |   11.1084 |     - |     - |   35050 B |
        |                     |         |                  |                 |                 |          |         |           |       |       |           |
        | 'Cartesian Cistern' |     100 |      1,036.78 ns |       6.9351 ns |       6.4871 ns |     0.30 |    0.00 |    0.0896 |     - |     - |     288 B |
        |    'Cartesian Linq' |     100 |      3,491.20 ns |      29.3154 ns |      27.4217 ns |     1.00 |    0.00 |    0.1640 |     - |     - |     520 B |
        |     'Cartesian Opt' |     100 |    109,082.81 ns |     813.5949 ns |     761.0372 ns |    31.25 |    0.25 |   11.1084 |     - |     - |   35050 B |
        |                     |         |                  |                 |                 |          |         |           |       |       |           |
        | 'Cartesian Cistern' |   10000 |     86,116.90 ns |     686.6200 ns |     642.2648 ns |     0.26 |    0.00 |         - |     - |     - |     288 B |
        |    'Cartesian Linq' |   10000 |    337,411.42 ns |   3,040.0049 ns |   2,694.8852 ns |     1.00 |    0.00 |    9.7656 |     - |     - |   32200 B |
        |     'Cartesian Opt' |   10000 |    138,558.95 ns |   1,154.6305 ns |   1,023.5498 ns |     0.41 |    0.00 |   10.9863 |     - |     - |   35050 B |
        |                     |         |                  |                 |                 |          |         |           |       |       |           |
        | 'Cartesian Cistern' | 1000000 |  8,606,452.60 ns |  97,523.0158 ns |  91,223.0895 ns |     0.25 |    0.00 |         - |     - |     - |     288 B |
        |    'Cartesian Linq' | 1000000 | 33,758,322.92 ns | 373,052.7235 ns | 348,953.7490 ns |     1.00 |    0.00 | 1000.0000 |     - |     - | 3200200 B |
        |     'Cartesian Opt' | 1000000 |  2,878,193.54 ns |  19,063.2906 ns |  17,831.8138 ns |     0.09 |    0.00 |    7.8125 |     - |     - |   35052 B |*/
        public partial class CartesianBenchmarks : BenchmarkBase
        {
            [Benchmark(Description = "Cartesian Cistern")]
            public double CartCistern()
            {
                return (from x in dim1
                        from y in dim2
                        select x * y).Sum();
            }
        }

        /*
        |             Method |   Count |             Mean |             Error |            StdDev |    Ratio | RatioSD |      Gen 0 |     Gen 1 |     Gen 2 |   Allocated |
        |------------------- |-------- |-----------------:|------------------:|------------------:|---------:|--------:|-----------:|----------:|----------:|------------:|
        | 'Group By Cistern' |       0 |         393.8 ns |          1.753 ns |          1.554 ns |     1.48 |    0.01 |     0.2208 |         - |         - |       696 B |
        |    'Group By Linq' |       0 |         265.7 ns |          2.048 ns |          1.815 ns |     1.00 |    0.00 |     0.1168 |         - |         - |       368 B |
        |     'Group By Opt' |       0 |     399,551.1 ns |      2,872.557 ns |      2,686.992 ns | 1,503.77 |   18.07 |    39.0625 |         - |         - |    124235 B |
        |                    |         |                  |                   |                   |          |         |            |           |           |             |
        | 'Group By Cistern' |      10 |       3,142.9 ns |         25.285 ns |         23.652 ns |     1.24 |    0.02 |     0.8087 |         - |         - |      2552 B |
        |    'Group By Linq' |      10 |       2,535.5 ns |         22.280 ns |         20.841 ns |     1.00 |    0.00 |     0.6523 |         - |         - |      2056 B |
        |     'Group By Opt' |      10 |     405,738.9 ns |      2,854.742 ns |      2,670.328 ns |   160.03 |    1.71 |    46.3867 |         - |         - |    147332 B |
        |                    |         |                  |                   |                   |          |         |            |           |           |             |
        | 'Group By Cistern' |     100 |      25,933.3 ns |        133.420 ns |        124.801 ns |     1.14 |    0.01 |     4.7607 |         - |         - |     15056 B |
        |    'Group By Linq' |     100 |      22,751.6 ns |        157.226 ns |        147.069 ns |     1.00 |    0.00 |     4.4861 |         - |         - |     14192 B |
        |     'Group By Opt' |     100 |     445,883.5 ns |      4,056.576 ns |      3,794.524 ns |    19.60 |    0.23 |    53.2227 |         - |         - |    168420 B |
        |                    |         |                  |                   |                   |          |         |            |           |           |             |
        | 'Group By Cistern' |   10000 |   5,212,502.0 ns |     26,990.373 ns |     25,246.812 ns |     0.96 |    0.01 |   242.1875 |  148.4375 |   78.1250 |   1497438 B |
        |    'Group By Linq' |   10000 |   5,411,742.6 ns |     26,162.135 ns |     24,472.078 ns |     1.00 |    0.00 |   226.5625 |  109.3750 |   46.8750 |   1405855 B |
        |     'Group By Opt' |   10000 |   6,185,879.8 ns |     38,216.056 ns |     35,747.323 ns |     1.14 |    0.01 |   398.4375 |  296.8750 |  296.8750 |   3016919 B |
        |                    |         |                  |                   |                   |          |         |            |           |           |             |
        | 'Group By Cistern' | 1000000 | 721,762,426.7 ns | 11,219,209.587 ns | 10,494,455.608 ns |     0.93 |    0.01 | 12000.0000 | 6000.0000 | 2000.0000 | 109197016 B |
        |    'Group By Linq' | 1000000 | 777,703,673.3 ns |  7,380,273.468 ns |  6,903,512.381 ns |     1.00 |    0.00 | 13000.0000 | 6000.0000 | 2000.0000 | 104646152 B |
        |     'Group By Opt' | 1000000 | 780,702,233.3 ns | 12,137,050.645 ns | 11,353,004.704 ns |     1.00 |    0.01 | 16000.0000 | 6000.0000 | 2000.0000 | 169265304 B |
        */
        public partial class GroupByBenchmarks : BenchmarkBase
        {
            [Benchmark(Description = "Group By Cistern")]
            public int[] GroupCistern()
            {
                return values
                    .GroupBy(x => (int)x / 100)
                    .OrderBy(x => x.Key)
                    .Select(k => k.Count())
                    .ToArray();
            }
        }

        /*
        |                        Method |  max |               Mean |             Error |            StdDev |    Ratio | RatioSD |        Gen 0 | Gen 1 | Gen 2 |    Allocated |
        |------------------------------ |----- |-------------------:|------------------:|------------------:|---------:|--------:|-------------:|------:|------:|-------------:|
        | 'Pythagorean Triples Cistern' |    0 |           527.5 ns |          3.755 ns |          3.513 ns |     1.66 |    0.01 |       0.2508 |     - |     - |        792 B |
        |    'Pythagorean Triples Linq' |    0 |           318.4 ns |          2.403 ns |          2.130 ns |     1.00 |    0.00 |       0.1574 |     - |     - |        496 B |
        |     'Pythagorean Triples Opt' |    0 |       348,225.3 ns |      2,112.988 ns |      1,976.491 ns | 1,094.57 |    9.41 |      30.7617 |     - |     - |      98062 B |
        |                               |      |                    |                   |                   |          |         |              |       |       |              |
        | 'Pythagorean Triples Cistern' |   10 |         8,285.1 ns |         84.632 ns |         79.165 ns |     0.80 |    0.01 |       3.5706 |     - |     - |      11280 B |
        |    'Pythagorean Triples Linq' |   10 |        10,385.1 ns |         76.444 ns |         71.506 ns |     1.00 |    0.00 |       3.6316 |     - |     - |      11456 B |
        |     'Pythagorean Triples Opt' |   10 |       351,490.9 ns |      2,800.651 ns |      2,619.731 ns |    33.85 |    0.38 |      30.7617 |     - |     - |      98070 B |
        |                               |      |                    |                   |                   |          |         |              |       |       |              |
        | 'Pythagorean Triples Cistern' |  100 |     2,964,882.8 ns |     17,915.109 ns |     16,757.804 ns |     0.63 |    0.01 |    1835.9375 |     - |     - |    5781240 B |
        |    'Pythagorean Triples Linq' |  100 |     4,695,351.8 ns |     44,031.952 ns |     41,187.515 ns |     1.00 |    0.00 |    1843.7500 |     - |     - |    5822096 B |
        |     'Pythagorean Triples Opt' |  100 |       568,447.1 ns |      4,059.144 ns |      3,598.326 ns |     0.12 |    0.00 |      30.2734 |     - |     - |      98070 B |
        |                               |      |                    |                   |                   |          |         |              |       |       |              |
        | 'Pythagorean Triples Cistern' | 1000 | 2,469,709,900.0 ns |  8,300,441.640 ns |  7,358,125.312 ns |     0.58 |    0.00 | 1709000.0000 |     - |     - | 5377404840 B |
        |    'Pythagorean Triples Linq' | 1000 | 4,271,984,966.7 ns | 29,321,501.294 ns | 27,427,350.503 ns |     1.00 |    0.00 | 1710000.0000 |     - |     - | 5381416496 B |
        |     'Pythagorean Triples Opt' | 1000 |   173,111,693.3 ns |  1,882,627.953 ns |  1,761,011.356 ns |     0.04 |    0.00 |            - |     - |     - |     100344 B |
        */
        public partial class PythagoreanTriplesBenchmarks
        {
            [Benchmark(Description = "Pythagorean Triples Cistern")]
            public int PythagoreanTriplesCistern()
            {
                return (from a in Enumerable.Range(1, max + 1)
                        from b in Enumerable.Range(a, max + 1 - a)
                        from c in Enumerable.Range(b, max + 1 - b)
                        where a * a + b * b == c * c
                        select true).Count();
            }
        }
    }
}
