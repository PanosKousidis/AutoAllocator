using AutoAllocator.Logic;
using AutoAllocator.Logic.Services;

namespace AutoAllocator.Console;

public static class Program
{
    public static void Main(string[] args)
    {
        var executor = new AllocationExecutor(
            new CsvParserFromFile(), 
            new SimpleAllocatorWithUtilisationPenalty(),
            new CsvOutputGenerator(),
            args);
        
        executor.Execute();
    }
}