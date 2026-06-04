using BenchmarkDotNet.Attributes;
using MaximPractice.Algorithms;
using MaximPractice.Data;
using MaximPractice.Interfaces;
using System;
using System.Collections.Generic;

namespace MaximPractice.Benchmarks;

[MemoryDiagnoser]
public class Benchmarks
{
    private Map _map = null;

    [ParamsSource(nameof(Algorithms))]
    public IDriverSearchAlgorithm Algorithm;

    public IEnumerable<IDriverSearchAlgorithm> Algorithms() => AlgorithmsRepo.Algorithms;

    [Params(100, 500, 1000, 10000, 100000, 500000)]
    public int DriverCount;

    [GlobalSetup]
    public void Setup()
    {
        _map = new Map(1000, 1000);

        var random = new Random(42);
        var emptyCoords = new List<(int, int)>();
        for (int x = 0; x < 1000; x++)
        {
            for (int y = 0; y < 1000; y++)
            {
                emptyCoords.Add((x, y));
            }
        }

        for (int i = emptyCoords.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);

            (emptyCoords[i], emptyCoords[j]) = (emptyCoords[j], emptyCoords[i]);
        }

        for (int i = 0; i < DriverCount; i++)
        {
            var newCoord = emptyCoords[i];
            _map.AddDriver(
                i,
                new Point(
                    newCoord.Item1,
                    newCoord.Item2
                    )
            );
        }
    }

    [Benchmark]
    public Driver[] Run()
    {
        return Algorithm.FindNearestDrivers(
            _map,
            new Point(500, 500)
            );
    }
}
