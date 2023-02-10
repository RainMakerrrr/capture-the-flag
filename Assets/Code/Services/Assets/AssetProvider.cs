using UnityEngine;

namespace Code.Services.Assets
{ 
    public class AssetProvider : IAssetProvider
    {
        public T Load<T>(string path) where T : Component => Resources.Load<T>(path);
    }
}