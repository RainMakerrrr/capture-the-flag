using System;
using Code.Services.Input;
using UnityEngine;

namespace Code.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;

        private IInputService _inputService;

        private void Start()
        {
            _inputService = new JoystickInputService(_joystick);
        }

        private void Update()
        {
            transform.Translate(_inputService.Movement * 5f * Time.deltaTime);
            //transform.position += _input.Movement * 5f;
        }
    }
}