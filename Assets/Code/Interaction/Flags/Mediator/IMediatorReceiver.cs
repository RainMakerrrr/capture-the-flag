namespace Code.Interaction.Flags.Mediator
{
    public interface IMediatorReceiver
    {
        string Id { get; }
        void Notify(MessageType messageType, string id = "");
    }
}