using Code.Interaction.Flags;
using Code.Interaction.Flags.Mediator;
using Code.Player;
using Code.Services.Assets;
using UnityEngine;

namespace Code.Services.Factories
{
    public class FlagFactory : IFlagFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly Transform _playerTransform;
        private readonly IMediator _mediator;
        private readonly IFlagInvader _flagInvader;
        private readonly float _radius;
        private readonly float _capturingTime;
        private readonly float _miniGameCallAttemptRate;

        private Flag _flagPrefab;

        public FlagFactory(IAssetProvider assetProvider, Transform playerTransform, IMediator mediator,
            IFlagInvader flagInvader, float radius,
            float capturingTime, float miniGameCallAttemptRate)
        {
            _playerTransform = playerTransform;
            _assetProvider = assetProvider;
            _mediator = mediator;
            _flagInvader = flagInvader;
            _radius = radius;
            _capturingTime = capturingTime;
            _miniGameCallAttemptRate = miniGameCallAttemptRate;
        }

        public Flag Create()
        {
            _flagPrefab ??= _assetProvider.Load<Flag>(AssetPath.FlagPrefab);
            Flag flag = Object.Instantiate(_flagPrefab);

            flag.Construct(_playerTransform, _mediator, _radius, _capturingTime, _flagInvader, _miniGameCallAttemptRate);

            return flag;
        }

        public Flag Create(Vector3 position, Transform parent)
        {
            _flagPrefab ??= _assetProvider.Load<Flag>(AssetPath.FlagPrefab);
            Flag flag = Object.Instantiate(_flagPrefab, position, Quaternion.identity, parent);

            flag.Construct(_playerTransform, _mediator, _radius, _capturingTime, _flagInvader, _miniGameCallAttemptRate);

            return flag;
        }
    }
}