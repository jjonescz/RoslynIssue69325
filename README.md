```ps1
dotnet run -c Release -f net8.0 --filter '**'
```

## Results

```log
BenchmarkDotNet=v0.13.5, OS=Windows 11 (10.0.22631.3007), VM=Hyper-V
Intel Xeon Platinum 8370C CPU 2.80GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  Job-WJVUQY : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  Job-PMTJCT : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2

Runtime=.NET 8.0  
```

|   Method |        Job |  BuildConfiguration |       Mean | Ratio | Code Size |
|--------- |----------- |-------------------- |-----------:|------:|----------:|
|    Run_3 | Job-WJVUQY |             Release |   2.372 ns |  1.00 |     117 B |
|    Run_3 | Job-PMTJCT | ReleaseCustomRoslyn |   2.307 ns |  0.97 |     114 B |
|          |            |                     |            |       |           |
| Run_1000 | Job-WJVUQY |             Release | 164.043 ns |  1.00 |  10,023 B |
| Run_1000 | Job-PMTJCT | ReleaseCustomRoslyn |  60.540 ns |  0.37 |     149 B |

