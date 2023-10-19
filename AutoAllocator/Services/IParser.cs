using System.Dynamic;
using System.Globalization;
using CsvHelper;

namespace AutoAllocator;

public interface IParser
{
    List<Student> ParseStudents(string file);
    List<Supervisor> ParseSupervisors(string file);
}

public class CsvParser : IParser
{
    public List<Student> ParseStudents(string file)
    {
        var records = GetRecords<dynamic>(file);
        var students = records.Select(x => new Student
            {
                Name = x.Name,
                Topics = GetTopics(x)
            })
            .ToList();
        return students;
    }

    public List<Supervisor> ParseSupervisors(string file)
    {
        var records = GetRecords<dynamic>(file);
        var supervisors = records.Select(x => new Supervisor
            {
                Name = x.Name,
                Capacity = int.TryParse(x.Capacity, out int i) ? i : 0,
                Topics = GetTopics(x)
            })
            .ToList();
        return supervisors;
    }

    private static List<T> GetRecords<T>(string file)
    {
        using var reader = new StreamReader(file,
            new FileStreamOptions { Access = FileAccess.Read, Share = FileShare.ReadWrite });
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<T>();
        return records.ToList();
    }

    private static string[] GetTopics(ExpandoObject o)
    {
        var topics = o
            .Where(x => x.Value?.ToString()?.Equals("X", StringComparison.OrdinalIgnoreCase) == true)
            .Select(x => x.Key);
        return topics.ToArray();
    }
}
