namespace AutoAllocator.Logic.Model
{

    public class Supervisor
    {
        public string? Name { get; set; }
        public string[]? Topics { get; set; }
        public int Capacity { get; set; }
        public int AvailableCapacity => Capacity - SlotsTaken;
        public int SlotsTaken { get; set; }

        public override string ToString()
        {
            return $"{Name} ({SlotsTaken}/{Capacity} ({string.Join(",", Topics ?? new[] { "None" })})";
        }
    }
}