using Code.Player;
using Code.Services.Assets;
using Code.Services.Input;
using UnityEngine;

namespace Code.Services.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IInputService _inputService;
        private readonly float _moveSpeed;
        private readonly float _banTime;

        public PlayerFactory(IAssetProvider assetProvider, IInputService inputService, float moveSpeed, float banTime)
        {
            _assetProvider = assetProvider;
            _inputService = inputService;
            _moveSpeed = moveSpeed;
            _banTime = banTime;
        }

        public GameObject CreatePlayer()
        {
            var prefab = _assetProvider.Load<GameObject>(AssetPath.PlayerPrefab);

            GameObject player = Object.Instantiate(prefab);
            player.GetComponent<PlayerMovement>().Construct(_inputService, _moveSpeed);
            player.GetComponent<FlagInvader>().Construct(_banTime);

            return player;
        }
    }
}