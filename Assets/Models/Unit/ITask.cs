public interface ITask
{
    Unit Unit { get; set; }
    TaskType Type { get; }
    TaskState State { get; set; }
}
