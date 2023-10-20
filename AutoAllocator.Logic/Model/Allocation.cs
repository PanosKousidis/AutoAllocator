namespace AutoAllocator.Logic.Model
{


    public class Allocation
    {
        public Allocation(Supervisor supervisor, Student student)
        {
            Student = student;
            Supervisor = supervisor;
        }

        public Student Student { get; }
        public Supervisor Supervisor { get; }

        public override string ToString()
        {
            return $"{Student.Name} --> {Supervisor.Name}";
        }
    }
}