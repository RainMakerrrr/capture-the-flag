using UnityEngine;

namespace Code.Services.Assets
{ 
    public class AssetProvider : IAssetProvider
    {
        public T Load<T>(string path) where T : Object => Resources.Load<T>(path);
    }
}