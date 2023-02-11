using UnityEngine;

namespace Code.Services.Timer
{
    public class TimerService : ITimerService
    {
        public float Current { get; private set; }

        public float Max => _targetTime;
        public bool IsStopped => _isStopped;

        private readonly float _targetTime;
        private bool _isStopped;

        public TimerService(float targetTime)
        {
            _targetTime = targetTime;
        }

        public void Reset() => Current = 0;

        public void Tick()
        {
            if (_isStopped == false && IsTimerEnd() == false)
                Current += Time.deltaTime;
        }

        public void Stop() => _isStopped = true;

        public void Resume() => _isStopped = false;

        public bool IsTimerEnd() => Current >= _targetTime;
    }
}