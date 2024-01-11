using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public partial class Program
{
	static void Main(string[] args)
	{
		var jobs =
			from runtime in new[] { CoreRuntime.Core80 }
            from config in new[] { "Release", "ReleaseCustomRoslyn" }
			select Job.Default.WithRuntime(runtime)
                .WithCustomBuildConfiguration(config)
                .WithBaseline(runtime == CoreRuntime.Core80 && config == "Release");

		BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, ManualConfig.CreateMinimumViable()
			.HideColumns("Error", "StdDev", "Median", "RatioSD", "x", "y", "c")
			.AddDiagnoser(new DisassemblyDiagnoser(new DisassemblyDiagnoserConfig(
				printSource: true,
				exportHtml: true,
				exportCombinedDisassemblyReport: true,
				exportDiff: true)))
			.AddJob(jobs.ToArray()));
	}

	[Benchmark]
	public void Run() => Use(stackalloc int[3] { 1, 2, 3 });

    [MethodImpl(MethodImplOptions.NoInlining)]
    static void Use(Span<int> span) { }
}
