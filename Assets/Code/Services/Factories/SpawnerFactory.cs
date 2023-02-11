using Code.Services.Assets;
using UnityEngine;

namespace Code.Services.Factories
{
    public class SpawnerFactory : ISpawnerFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IFlagFactory _flagFactory;
        private readonly int _flagCount;

        public SpawnerFactory(IAssetProvider assetProvider, IFlagFactory flagFactory, int flagCount)
        {
            _assetProvider = assetProvider;
            _flagFactory = flagFactory;
            _flagCount = flagCount;
        }

        public FlagSpawner CreateFlagSpawner()
        {
            var flagSpawnerPrefab = _assetProvider.Load<FlagSpawner>(AssetPath.FlagSpawner);
            FlagSpawner flagSpawner = Object.Instantiate(flagSpawnerPrefab);

            flagSpawner.Construct(_flagFactory, _flagCount);

            return flagSpawner;
        }
    }
}