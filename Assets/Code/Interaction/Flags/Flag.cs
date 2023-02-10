using UnityEngine;

namespace Code.Interaction.Flags
{
    public class Flag : MonoBehaviour
    {
        private FlagState _state;
        
        private Transform _playerTransform;
        private float _radius;

        public void Construct(Transform playerTransform, float radius)
        {
            _playerTransform = playerTransform;
            _radius = radius;
        }

        private void Update()
        {
            
        }

        private bool IsPlayerInRadius() => Vector3.Distance(transform.position, _playerTransform.position) <= _radius;
    }
}