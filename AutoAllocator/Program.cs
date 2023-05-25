namespace AutoAllocator;

public static class Program
{
    public static void Main(string[] args)
    {
        IAllocationExecutor executor = new AllocationExecutor();
        executor.Execute(
            new CsvParser(),
            new SimpleAllocatorWithUtilisationPenalty(),
            args);
    }
}