using UnityEngine;

namespace Code.Services.Factories
{
    public interface IPlayerFactory
    {
        GameObject CreatePlayer();
    }
}