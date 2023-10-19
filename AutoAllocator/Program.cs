namespace AutoAllocator;

public static class Program
{
    public static void Main(string[] args)
    {
        var executor = new AllocationExecutor(
            new CsvParser(), 
            new SimpleAllocatorWithUtilisationPenalty(),
            new CsvOutputGenerator());
        executor.Execute(args);
    }
}