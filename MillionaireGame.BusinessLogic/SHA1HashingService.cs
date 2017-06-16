using System.Security.Cryptography;
using System.Text;

// ReSharper disable InconsistentNaming

namespace MillionaireGame.BusinessLogic
{
    public class SHA1HashingService : IHashingService
    {
        public string GetPasswordHash(string password)
        {
            using (var hasher = new SHA1Managed())
            {
                var hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(password));
                var resultHash = new StringBuilder(hash.Length * 2);

                foreach (var b in hash)
                {
                    resultHash.Append(b.ToString("X2"));
                }

                return resultHash.ToString();
            }
        }
    }
}
