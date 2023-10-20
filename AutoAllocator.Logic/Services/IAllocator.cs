using System.Collections.Generic;
using System.Linq;
using AutoAllocator.Logic.Model;

namespace AutoAllocator.Logic.Services
{

    public interface IAllocator
    {
        IEnumerable<Allocation> Allocate(List<Supervisor> supervisors, List<Student> students);
    }

    public class SimpleAllocatorWithUtilisationPenalty : IAllocator
    {
        public IEnumerable<Allocation> Allocate(List<Supervisor> supervisors, List<Student> students)
        {
            var allocations = new List<Allocation>();
            foreach (var supervisor in supervisors)
            {
                supervisor.SlotsTaken = 0;
            }

            foreach (var student in students)
            {
                var supervisorAllocations = supervisors.Select(supervisor => new
                    {
                        Supervisor = supervisor,
                        Student = student,
                        CommonTopics = supervisor.Topics?.Intersect(student.Topics ?? Enumerable.Empty<string>())
                            .Count(),
                        UtilizationPenalty = 100.0 * supervisor.SlotsTaken / supervisor.Capacity
                    })
                    .Where(x => x.Supervisor.AvailableCapacity > 0)
                    .OrderByDescending(x => x.CommonTopics)
                    .ThenBy(x => x.UtilizationPenalty)
                    .FirstOrDefault();

                if (supervisorAllocations == null) continue;
                var supervisor = supervisorAllocations.Supervisor;
                allocations.Add(new Allocation(supervisor, student));
                supervisor.SlotsTaken++;
            }

            return allocations;
        }
    }
}