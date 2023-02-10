using UnityEngine;

namespace Code.Services.Timer
{
    public class TimerService : ITimerService
    {
        public float Timer { get; private set; }

        private readonly float _targetTime;

        public TimerService(float targetTime)
        {
            _targetTime = targetTime;
        }

        public void Reset() => Timer = 0;

        public void Tick() => Timer += Time.deltaTime;

        public bool IsTimerEnd() => Timer >= _targetTime;
    }
}