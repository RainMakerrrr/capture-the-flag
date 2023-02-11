using System;
using System.Collections;
using Code.Interaction.Flags.Mediator;
using UnityEngine;

namespace Code.Player
{
    public class FlagInvader : MonoBehaviour, IFlagInvader, IMediatorReceiver
    {
        public event Action<float> Banned;
        
        public string Id => GetInstanceID().ToString();
        public bool CanInvade { get; private set; } = true;

        private float _banTime; 

        public void Construct(float banTime)
        {
            _banTime = banTime;
        }

        public void Notify(MessageType messageType, string id = "")
        {
            if (messageType == MessageType.Failure)
            {
                CanInvade = false;
                StartCoroutine(WaitForBanExpired());
                Banned?.Invoke(_banTime);
            }
        }

        private IEnumerator WaitForBanExpired()
        {
            yield return new WaitForSeconds(_banTime);
            CanInvade = true;
        }
    }
}