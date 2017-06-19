namespace MillionaireGame.BusinessLogic
{
    public interface IHashingService
    {
        string GetPasswordHash(string password);
    }
}
