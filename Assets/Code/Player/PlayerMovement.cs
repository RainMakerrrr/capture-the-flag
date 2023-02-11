using Code.Services.Input;
using UnityEngine;

namespace Code.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private IInputService _inputService;
        private float _moveSpeed;
        
        public void Construct(IInputService inputService, float moveSpeed)
        {
            _inputService = inputService;
            _moveSpeed = moveSpeed;
        }

        private void Update() => Move();

        private void Move() => transform.Translate(_inputService.Movement * (_moveSpeed * Time.deltaTime));
    }
}