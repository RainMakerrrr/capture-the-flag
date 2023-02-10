using System;
using System.Collections.Generic;
using System.Linq;
using Code.Interaction;
using Code.Interaction.Flags;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code
{
    public class FlagSpawner : MonoBehaviour
    {
        [SerializeField] private Flag _prefab;

        private List<Flag> _flags = new();

        private void Start()
        {
            SpawnFlags();
        }

        private void SpawnFlags()
        {
            for (int i = 0; i < 5; i++)
            {
                Vector3 position = Random.insideUnitSphere * 8f;
                position.y = 1f;
                
                Flag flagInstance = Instantiate(_prefab, position, Quaternion.identity, transform);
                
                if (_flags.Count > 0)
                {
                    while (IsFlagCloserToOthers(flagInstance))
                    {
                        position = Random.insideUnitSphere * 12f;
                        position.y = 1f;
                        flagInstance.transform.position = position;
                    }
                }
                
                _flags.Add(flagInstance);
            }
        }

        private bool IsFlagCloserToOthers(Flag flag)
        {
            Debug.Log(_flags);
            Debug.Log(_flags.Count);
            Debug.Log(flag);
            
            return _flags.Any(f => Vector3.Distance(f.transform.position, flag.transform.position) <= 8);
        }
    }
}