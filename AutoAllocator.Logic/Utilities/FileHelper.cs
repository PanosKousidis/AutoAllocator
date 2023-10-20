using System;
using System.IO;

namespace AutoAllocator.Logic.Utilities
{

    public class FileHelper
    {
        public static (string supervisorsFilePath, string studentFilePath, string? outputFilePath) GetFilePaths(
            string[] args)
        {
            if (args.GetLength(0) == 2)
            {
                return (args[0], args[1], null);
            }

            Console.Write("Supervisors path : ");
            var supervisors = Console.ReadLine();
            Console.Write("Students path : ");
            var students = Console.ReadLine();
            Console.Write("Output path : ");
            var outputPath = Console.ReadLine();

            return supervisors != null && students != null
                ? (supervisors, students, outputPath)
                : throw new InvalidDataException("You need to supply the supervisors and students filenames");
        }

        public static void WriteFile(string result, string path)
        {
            using var sw = File.CreateText(path);
            sw.Write(result);
        }
    }
}