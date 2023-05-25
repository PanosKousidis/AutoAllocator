namespace AutoAllocator;

public interface IAllocationExecutor
{
    void Execute(IParser parser, IAllocator allocator, string[] args);
}

public class AllocationExecutor : IAllocationExecutor
{
    public void Execute(IParser parser, IAllocator allocator, string[] args)
    {
        var (supervisorsFilePath, studentFilePath) = FileHelper.GetFilePaths(args);
        var students = parser.ParseStudents(studentFilePath);
        var supervisors = parser.ParseSupervisors(supervisorsFilePath);
        var allocations = allocator.Allocate(supervisors, students);
        Console.WriteLine(allocations);
    }
}