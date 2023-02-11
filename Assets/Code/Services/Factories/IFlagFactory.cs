using Code.Interaction;
using Code.Interaction.Flags;
using UnityEngine;

namespace Code.Services.Factories
{
    public interface IFlagFactory
    {
        Flag Create();
        Flag Create(Vector3 position, Transform parent);
    }
}