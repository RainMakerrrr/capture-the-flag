using Code.Interaction;
using Code.Interaction.Flags;
using Code.Services.Assets;
using UnityEngine;

namespace Code.Services.Factories
{
    public class FlagFactory : IFlagFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly Transform _player;
        private readonly float _radius;
        
        private Flag _flagPrefab;

        public FlagFactory(IAssetProvider assetProvider,Transform player, float radius)
        {
            _player = player;
            _radius = radius;
            _assetProvider = assetProvider;
        }

        public Flag Create()
        {
            _flagPrefab ??= _assetProvider.Load<Flag>(AssetPath.FlagPrefab);
            Flag flag = Object.Instantiate(_flagPrefab);
            flag.Construct(_player, _radius);

            return flag;
        }
    }
}