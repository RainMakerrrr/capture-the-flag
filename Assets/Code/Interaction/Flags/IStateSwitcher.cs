using Code.Interaction.Flags.States;

namespace Code.Interaction.Flags
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : IFlagState;
    }
}