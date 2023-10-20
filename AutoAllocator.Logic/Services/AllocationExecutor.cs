using AutoAllocator.Logic.Utilities;

namespace AutoAllocator.Logic.Services
{
    public interface IAllocationExecutor
    {
        void Execute();
    }

    public class AllocationExecutor : IAllocationExecutor
    {
        private readonly IParser _parser;
        private readonly IAllocator _allocator;
        private readonly IOutputGenerator _outputGenerator;
        private readonly string[] _args;

        public AllocationExecutor(IParser parser, IAllocator allocator, IOutputGenerator outputGenerator, string[] args)
        {
            _parser = parser;
            _allocator = allocator;
            _outputGenerator = outputGenerator;
            _args = args;
        }

        public void Execute()
        {
            var (supervisorsFilePath, studentFilePath, outputFilePath) = FileHelper.GetFilePaths(_args);
            var students = _parser.ParseStudents(studentFilePath);
            var supervisors = _parser.ParseSupervisors(supervisorsFilePath);
            var allocations = _allocator.Allocate(supervisors, students);
            _outputGenerator.Generate(allocations, outputFilePath);
        }
    }
}