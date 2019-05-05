using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MacroDB.Util
{
    public static class Token
    {
        public static string createToken()
        {
            byte[] token = new byte[16];
            RNGCryptoServiceProvider gen = new RNGCryptoServiceProvider();
            gen.GetBytes(token);

            StringBuilder buf = new StringBuilder();
            for (int i = 0; i < token.Length; i++)
            {
                buf.AppendFormat("{0:x2}", token[i]);
            }
            return buf.ToString();
        }


    }

}