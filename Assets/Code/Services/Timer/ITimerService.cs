namespace Code.Services.Timer
{
    public interface ITimerService
    {
        float Timer { get; }
        void Reset();
        void Tick();
        bool IsTimerEnd();
    }
}