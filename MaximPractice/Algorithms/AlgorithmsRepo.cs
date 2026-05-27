using MaximPractice.Interfaces;

namespace MaximPractice.Algorithms;

public static class AlgorithmsRepo
{
    public static IReadOnlyCollection<IDriverSearchAlgorithm> Algorithms
        = new IDriverSearchAlgorithm[] {
            new LinearSearch(),
            new TopKSearch(),
            new RadiusSearch(),
            new MatrixSearch()
        };
}
