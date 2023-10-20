using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoAllocator.Logic.Model;
using AutoAllocator.Logic.Utilities;

namespace AutoAllocator.Logic.Services
{

    public interface IOutputGenerator
    {
        void Generate(IEnumerable<Allocation> allocations, string? outputFile = null);
    }

    public class TextOutputGenerator : IOutputGenerator
    {
        public void Generate(IEnumerable<Allocation> allocations, string? outputFile = null)
        {
            var sb = new StringBuilder();
            var grouped = allocations.GroupBy(x => x.Supervisor, allocation => allocation.Student);
            foreach (var group in grouped)
            {
                sb.AppendLine(group.Key.ToString());
                foreach (var student in group)
                {
                    sb.AppendLine($"\t{student.ToString()}");
                }

                sb.AppendLine();
            }

            var result = sb.ToString();
            Console.WriteLine(result);
            if (outputFile != null) FileHelper.WriteFile(result, outputFile);
        }
    }

    public class CsvOutputGenerator : IOutputGenerator
    {
        public void Generate(IEnumerable<Allocation> allocations, string? outputFile = null)
        {
            var sb = new StringBuilder();
            var items = allocations.Select(x =>
                (x.Student.Name, x.Student.Topics?.FirstOrDefault(), x.Supervisor.Name));
            foreach (var item in items)
            {
                sb.AppendLine(item.Item1 + "," + item.Item2 + "," + item.Item3);
            }

            var result = sb.ToString();
            Console.WriteLine(result);
            if (!string.IsNullOrWhiteSpace(outputFile)) FileHelper.WriteFile(result, outputFile);
        }
    }
}