namespace Code.Interaction.Flags.States
{
    public interface IFlagState
    {
        void Enter();
        void Tick();
        void Exit();
    }
}