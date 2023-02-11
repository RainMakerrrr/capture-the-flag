using Code.Interaction.Flags.UI;

namespace Code.Interaction.Flags.States
{
    public class CapturedState : IFlagState
    {
        private readonly FlagMaterialSwitcher _materialSwitcher;
        private readonly FlagPassiveView _view;

        public CapturedState(FlagMaterialSwitcher materialSwitcher, FlagPassiveView view)
        {
            _materialSwitcher = materialSwitcher;
            _view = view;
        }

        public void Enter()
        {
            _materialSwitcher.SwitchMaterial();
            _view.SetMaxValue();
            _view.DisableBar();
        }

        public void Tick()
        {
        }

        public void Exit()
        {
        }
    }
}