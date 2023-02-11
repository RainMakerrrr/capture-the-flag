using Code.Interaction.Flags.Mediator;
using Code.Interaction.Flags.UI;
using Code.Player;
using Code.Services.Timer;
using UnityEngine;

namespace Code.Interaction.Flags.States
{
    public class CapturingState : IFlagState
    {
        private readonly ITimerService _timer;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IMediator _mediator;
        private readonly IMediatorReceiver _receiver;
        private readonly IFlagInvader _flagInvader;
        private readonly FlagPassiveView _view;
        private readonly Transform _transform;
        private readonly Transform _playerTransform;
        private readonly float _radius;

        private readonly float _attemptRate;
        private float _nextAttemptTime;

        public CapturingState(IMediatorReceiver receiver, IStateSwitcher stateSwitcher, ITimerService timer,
            IMediator mediator, FlagPassiveView view, Transform transform, Transform playerTransform,IFlagInvader flagInvader ,float attemptRate,
            float radius)
        {
            _timer = timer;
            _stateSwitcher = stateSwitcher;
            _mediator = mediator;
            _receiver = receiver;
            _view = view;
            _transform = transform;
            _playerTransform = playerTransform;
            _flagInvader = flagInvader;
            _attemptRate = attemptRate;
            _radius = radius;
        }

        public void Enter()
        {
            _view.EnableBar();
        }

        public void Tick()
        {
            if (IsPlayerInRadius() == false)
            {
                _stateSwitcher.SwitchState<FreeState>();
                _view.DisableBar();
            }

            TickCapturingTimer();
        }

        public void Exit()
        {
        }

        private void TickCapturingTimer()
        {
            if (_timer.IsStopped || _flagInvader.CanInvade == false) return;

            _view.SetValue(_timer.Current, _timer.Max);
            _timer.Tick();

            TryCallMiniGame();

            if (_timer.IsTimerEnd())
            {
                _stateSwitcher.SwitchState<CapturedState>();
            }
        }

        private bool IsPlayerInRadius() => Vector3.Distance(_transform.position, _playerTransform.position) <= _radius;

        private void TryCallMiniGame()
        {
            if (_timer.Current < 1) return;
            if (!(Time.time > _nextAttemptTime)) return;

            _nextAttemptTime = Time.time + 1 / _attemptRate;

            int random = Random.Range(-1, 1);

            if (random == 0)
            {
                _timer.Stop();
                _mediator.Send(MessageType.Success, _receiver, _receiver.Id);
            }
        }
    }
}