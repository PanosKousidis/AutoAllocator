namespace AutoAllocator;

public interface IAllocator
{
    AllocationCollection Allocate(List<Supervisor> supervisors, List<Student> students);
}

public class SimpleAllocatorWithUtilisationPenalty : IAllocator
{
    public AllocationCollection Allocate(List<Supervisor> supervisors, List<Student> students)
    {
        var allocations = new List<Allocation>();

        foreach (var student in students)
        {
            var supervisorAllocations = supervisors.Select(supervisor => new
                {
                    Supervisor = supervisor,
                    Student = student,
                    CommonTopics = supervisor.Topics.Intersect(student.Topics).Count(),
                    UtilizationPenalty = 100.0 * supervisor.SlotsTaken / supervisor.Capacity
                })
                .Where(x => x.Supervisor.AvailableCapacity > 0)
                .OrderByDescending(x => x.CommonTopics)
                .ThenBy(x => x.UtilizationPenalty)
                .FirstOrDefault();

            if (supervisorAllocations != null)
            {
                var supervisor = supervisorAllocations.Supervisor;
                allocations.Add(new Allocation(supervisor, student));
                supervisor.SlotsTaken++;
            }
        }

        return new AllocationCollection(allocations);
    }
}