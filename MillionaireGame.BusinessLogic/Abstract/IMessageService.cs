namespace MillionaireGame.BusinessLogic.Abstract
{
    public interface IMessageService
    {
        void SendMessage(string text, string recipient);
    }
}
