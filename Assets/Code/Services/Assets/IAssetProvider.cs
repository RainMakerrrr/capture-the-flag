using UnityEngine;

namespace Code.Services.Assets
{
    public interface IAssetProvider
    {
        T Load<T>(string path) where T : Object;
    }
}