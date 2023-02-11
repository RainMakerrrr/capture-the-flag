using UnityEngine;

namespace Code.Services.Factories
{
    public interface ISpawnerFactory
    {
        FlagSpawner CreateFlagSpawner();
    }
}