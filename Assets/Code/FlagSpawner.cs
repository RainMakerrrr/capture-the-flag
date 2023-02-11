using System.Collections.Generic;
using System.Linq;
using Code.Interaction.Flags;
using Code.Player;
using Code.Services.Factories;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code
{
    public class FlagSpawner : MonoBehaviour
    {
        [SerializeField] private float _distance = 12f;
        [SerializeField] private float _radius = 15f;
        [SerializeField] private float _positionY = 0.3f;

        private readonly List<Flag> _flags = new();
        private IFlagFactory _factory;
        private int _flagsCount;

        public IReadOnlyList<Flag> Flags => _flags;

        public void Construct(IFlagFactory factory, int flagsCount)
        {
            _factory = factory;
            _flagsCount = flagsCount;
        }
        
        public void SpawnFlags()
        {
            Vector3 center = transform.position;

            for (int i = 0; i < _flagsCount; i++)
            {
                Vector3 position;

                do position = RandomOnSphere(center, _radius);
                while (AnyFlagCloserThan(position, _distance));

                position.y = _positionY;

                Flag flagInstance = _factory.Create(position, transform);
                flagInstance.name = $"flag {i}";

                _flags.Add(flagInstance);
            }
        }

        private bool AnyFlagCloserThan(Vector3 position, float minDistanceSqr) => _flags.Any(p =>
            Vector2.Distance(new Vector2(p.transform.position.x, p.transform.position.z),
                new Vector2(position.x, position.z)) <= minDistanceSqr);

        private Vector3 RandomOnSphere(Vector3 center, float radius) => center + Random.onUnitSphere * radius;
    }
}