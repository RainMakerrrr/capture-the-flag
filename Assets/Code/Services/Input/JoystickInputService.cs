using UnityEngine;

namespace Code.Services.Input
{
    public class JoystickInputService : IInputService
    {
        public Vector3 Movement => new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical);

        private readonly Joystick _joystick;

        public JoystickInputService(Joystick joystick)
        {
            _joystick = joystick;
        }
    }
}