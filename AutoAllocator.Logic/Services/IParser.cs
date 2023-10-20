using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using AutoAllocator.Logic.Model;
using CsvHelper;

namespace AutoAllocator.Logic.Services
{

    public interface IParser
    {
        List<Student> ParseStudents(string value);
        List<Supervisor> ParseSupervisors(string value);
    }


    public abstract class CsvParser
    {
        protected static List<T> GetRecords<T>(TextReader reader)
        {
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<T>();
            return records.ToList();
        }

        protected static string[] GetTopics(ExpandoObject o)
        {
            var topics = o
                .Where(x => x.Value?.ToString()?.Equals("X", StringComparison.OrdinalIgnoreCase) == true)
                .Select(x => x.Key);
            return topics.ToArray();
        }

        protected static List<Student> ParseStudentsFromList(IEnumerable<dynamic> records)
        {
            var students = records.Select(x => new Student
                {
                    Name = x.Name,
                    Topics = GetTopics(x)
                })
                .ToList();
            return students;
        }

        protected static List<Supervisor> ParseSupervisorsFromList(IEnumerable<dynamic> records)
        {
            var supervisors = records.Select(x => new Supervisor
                {
                    Name = x.Name,
                    Capacity = int.TryParse(x.Capacity, out int i) ? i : 0,
                    Topics = GetTopics(x)
                })
                .ToList();
            return supervisors;
        }
    }

    public class CsvParserFromString : CsvParser, IParser
    {
        public List<Student> ParseStudents(string contents)
        {
            var records = GetRecordsFromString<dynamic>(contents);
            return ParseStudentsFromList(records);
        }

        public List<Supervisor> ParseSupervisors(string contents)
        {
            var records = GetRecordsFromString<dynamic>(contents);
            return ParseSupervisorsFromList(records);
        }

        private static List<T> GetRecordsFromString<T>(string contents)
        {
            using var reader = new StringReader(contents);
            return GetRecords<T>(reader);
        }

    }

    public class CsvParserFromFile : CsvParser, IParser
    {
        public List<Student> ParseStudents(string value)
        {
            var records = GetRecordsFromFile<dynamic>(value);
            return ParseStudentsFromList(records);
        }

        public List<Supervisor> ParseSupervisors(string value)
        {
            var records = GetRecordsFromFile<dynamic>(value);
            return ParseSupervisorsFromList(records);
        }

        private static List<T> GetRecordsFromFile<T>(string file)
        {
            using var reader = new StreamReader(file);
            return GetRecords<T>(reader);
        }



    }
}