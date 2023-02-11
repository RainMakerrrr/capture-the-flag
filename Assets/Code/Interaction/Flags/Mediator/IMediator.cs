namespace Code.Interaction.Flags.Mediator
{
    public interface IMediator
    {
        void Send(MessageType messageType,IMediatorReceiver sender, string to = "");
    }
}