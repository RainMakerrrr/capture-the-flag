using UnityEngine;

namespace Code.Interaction.Flags
{
    public class FlagMaterialSwitcher : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Material _capturedMaterial;

        public void SwitchMaterial() => _renderer.material = _capturedMaterial;
    }
}