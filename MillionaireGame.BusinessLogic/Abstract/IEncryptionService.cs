namespace MillionaireGame.BusinessLogic.Abstract
{
    public interface IEncryptionService
    {
        string Encrypt(string source);
        string Decrypt(string cipher);
    }
}
