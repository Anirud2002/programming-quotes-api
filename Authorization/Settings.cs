using System.Text;

namespace ProgrammingQuotesApi.Authorization
{
    public static class Settings
    {
        private static readonly string _Secret = "BILO ŠTA SAMO DA NIJE PREKRATKO";
        public static byte[] Secret = Encoding.ASCII.GetBytes(_Secret);
    }
}