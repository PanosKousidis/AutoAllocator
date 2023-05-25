namespace AutoAllocator;

public class FileHelper
{
    public static (string supervisorsFilePath, string studentFilePath) GetFilePaths(string[] args)
    {
        if (args.GetLength(0) == 2)
        {
            return (args[0], args[1]);
        }
        Console.Write("Supervisors path : ");
        var supervisors = Console.ReadLine();
        Console.Write("Students path : ");
        var students = Console.ReadLine();
        return (supervisors, students);
    }
}