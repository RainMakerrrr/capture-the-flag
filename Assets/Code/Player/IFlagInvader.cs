using System;

namespace Code.Player
{
    public interface IFlagInvader
    {
        event Action<float> Banned;
        bool CanInvade { get; }
    }
}