public interface IStoppable
{
    bool IsStopped { get; }
    void Stop();
    void Resume();
}
