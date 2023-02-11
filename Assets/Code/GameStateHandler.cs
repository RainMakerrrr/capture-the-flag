using System.Collections.Generic;
using Code.Interaction.Flags;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public class GameStateHandler : MonoBehaviour
    {
        private const string MainSceneName = "Main Scene";
        
        private IReadOnlyList<Flag> _flags;
        private int _flagsCount;
        private int _capturedFlagsCount;
        
        public void Construct(IReadOnlyList<Flag> flags)
        {
            _flags = flags;
            _flagsCount = _flags.Count;
        }

        private void Start()
        {
            foreach (Flag flag in _flags)
            {
                flag.Captured += OnFlagCaptured;
            }
        }

        private void OnFlagCaptured()
        {
            _capturedFlagsCount++;

            if (_capturedFlagsCount == _flagsCount)
            {
                SceneManager.LoadScene(MainSceneName);
            }
        }

        private void OnDestroy()
        {
            foreach (Flag flag in _flags)
            {
                flag.Captured -= OnFlagCaptured;
            }
        }
    }
}