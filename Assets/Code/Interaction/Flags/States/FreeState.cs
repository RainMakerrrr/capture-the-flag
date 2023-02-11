using Code.Player;
using UnityEngine;

namespace Code.Interaction.Flags.States
{
    public class FreeState : IFlagState
    {
        private readonly Transform _transform;
        private readonly Transform _playerTransform;
        private readonly IFlagInvader _flagInvader;
        private readonly float _radius;
        private readonly IStateSwitcher _stateSwitcher;

        public FreeState(Transform transform, Transform playerTransform, IFlagInvader flagInvader, float radius,
            IStateSwitcher stateSwitcher)
        {
            _transform = transform;
            _playerTransform = playerTransform;
            _flagInvader = flagInvader;
            _radius = radius;
            _stateSwitcher = stateSwitcher;
        }

        public void Enter()
        {
        }

        public void Tick()
        {
            if (_playerTransform == null || _flagInvader.CanInvade == false) return;

            if (IsPlayerInRadius())
                _stateSwitcher.SwitchState<CapturingState>();
        }

        public void Exit()
        {
        }

        private bool IsPlayerInRadius() => Vector3.Distance(_transform.position, _playerTransform.position) <= _radius;
    }
}