namespace Intro.Services
{
    public class MbHasher : IHasher
    {
        public string Hash (string message)
        {
            using (var algo = 
                System.Security.Cryptography.MD5.Create())
            {
                byte[] hash = algo.ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(message));
                var sb = new System.Text.StringBuilder();
                foreach(byte b in hash)
                {
                    sb.Append(b.ToString("X02"));
                }
                return sb.ToString();
            }
        }
    }
}
