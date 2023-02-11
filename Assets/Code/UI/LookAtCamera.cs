using UnityEngine;

namespace Code.UI
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _mainCamera;

        private Camera Camera => _mainCamera ??= Camera.main;


        private void Update()
        {
            Quaternion rotation = Camera.transform.rotation;
            transform.LookAt(transform.position + rotation * -Vector3.back, rotation * Vector3.up);
        }
    }
}