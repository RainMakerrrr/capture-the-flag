namespace Code.Services.Timer
{
    public interface ITimerService
    {
        float Current { get; }
        float Max { get; }
        bool IsStopped { get; }
        void Reset();
        void Tick();
        bool IsTimerEnd();
        void Stop();
        void Resume();
    }
}