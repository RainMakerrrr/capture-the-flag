using System.Linq;
using UnityEngine;

namespace Code.Interaction.Flags.Mediator
{
    public class FlagMediator : IMediator
    {
        public IMediatorReceiver MiniGame { get; set; }
        public IMediatorReceiver Player { get; set; }
        public IMediatorReceiver[] Flags { get; set; }
        

        public void Send(MessageType messageType, IMediatorReceiver sender, string to = "")
        {
            if (sender == MiniGame)
            {
                IMediatorReceiver flag = Flags.FirstOrDefault(f => f.Id == to);

                flag?.Notify(messageType);
                Player.Notify(messageType);
            }
            else if (Flags.Any(flag => flag == sender))
            {
                IMediatorReceiver flag = Flags.FirstOrDefault(f => f == sender);
                
                MiniGame.Notify(messageType, flag?.Id);
            }
        }
    }
}