using System.Text;

namespace AutoAllocator;

public class AllocationCollection
{
    private readonly IEnumerable<Allocation> _allocations;

    public AllocationCollection(IEnumerable<Allocation> allocations)
    {
        _allocations = allocations;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        var grouped = _allocations.GroupBy(x => x.Supervisor, allocation => allocation.Student);
        foreach (var group in grouped)
        {
            sb.AppendLine(group.Key.ToString());
            foreach (var student in group)
            {
                sb.AppendLine($"\t{student.ToString()}");
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}