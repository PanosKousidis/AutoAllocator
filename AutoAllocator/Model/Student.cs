namespace AutoAllocator;

public class Student
{
    public string Name { get; set; }
    public string[] Topics { get; set; }

    public override string ToString()
    {
        return $"{Name} ({string.Join(",", Topics)})";
    }
}