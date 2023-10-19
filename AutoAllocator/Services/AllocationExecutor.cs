using System.Xml;

namespace AutoAllocator;

public class AllocationExecutor
{
    private readonly IParser _parser;
    private readonly IAllocator _allocator;
    private readonly IOutputGenerator _outputGenerator;

    public AllocationExecutor(IParser parser, IAllocator allocator, IOutputGenerator outputGenerator)
    {
        _parser = parser;
        _allocator = allocator;
        _outputGenerator = outputGenerator;
    }
    public void Execute(string[] args)
    {
        var (supervisorsFilePath, studentFilePath, outputFilePath) = FileHelper.GetFilePaths(args);
        var students = _parser.ParseStudents(studentFilePath);
        var supervisors = _parser.ParseSupervisors(supervisorsFilePath);
        var allocations = _allocator.Allocate(supervisors, students);
        _outputGenerator.Generate(allocations, outputFilePath);
    }
}