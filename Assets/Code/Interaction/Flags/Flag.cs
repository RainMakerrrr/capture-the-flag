using System;
using System.Collections.Generic;
using System.Linq;
using Code.Interaction.Flags.Mediator;
using Code.Interaction.Flags.States;
using Code.Interaction.Flags.UI;
using Code.Player;
using Code.Services.Timer;
using UnityEngine;

namespace Code.Interaction.Flags
{
    public class Flag : MonoBehaviour, IStateSwitcher, IMediatorReceiver
    {
        [SerializeField] private FlagPassiveView _view;
        [SerializeField] private FlagMaterialSwitcher _materialSwitcher;
        public event Action Captured;

        public string Id => GetInstanceID().ToString();

        private FlagState _state;

        private Transform _playerTransform;
        private IFlagState _currentState;
        private IMediator _mediator;
        private ITimerService _timerService;
        private IFlagInvader _flagInvader;

        private List<IFlagState> _states;

        private float _radius;
        private float _capturingTime;
        private float _miniGameCallAttemptRate;

        public void Construct(Transform playerTransform, IMediator mediator, float radius, float capturingTime,
            IFlagInvader flagInvader, float miniGameCallAttemptRate)
        {
            _playerTransform = playerTransform;
            _mediator = mediator;
            _radius = radius;
            _capturingTime = capturingTime;
            _flagInvader = flagInvader;
            _miniGameCallAttemptRate = miniGameCallAttemptRate;
        }

        private void Start()
        {
            InitTimerService();
            InitStates();
            _view.SetCapturedTime(_capturingTime);
        }


        private void Update() => _currentState?.Tick();

        private void InitStates()
        {
            _states = new List<IFlagState>
            {
                new FreeState(transform, _playerTransform, _flagInvader, _radius, this),
                new CapturingState(this, this, _timerService, _mediator, _view, transform, _playerTransform,
                    _flagInvader, _miniGameCallAttemptRate, _radius),
                new CapturedState(_materialSwitcher, _view)
            };

            _currentState = _states[0];
            _currentState.Enter();
        }

        private void InitTimerService() => _timerService = new TimerService(_capturingTime);

        public void SwitchState<T>() where T : IFlagState
        {
            IFlagState state = _states.FirstOrDefault(state => state is T);
            if (state == null) return;

            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();

            if (_currentState is CapturedState)
                Captured?.Invoke();
        }

        public void Notify(MessageType messageType, string id)
        {
            if (messageType == MessageType.Success)
                SwitchState<CapturedState>();
            else _timerService.Resume();
        }
    }
}