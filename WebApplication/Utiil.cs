namespace WebApplication
{
    public class Utiil
    {
        
    }

    public static class ByteArrayExtensions
    {
        public static string ByteArrayToString(this byte[] bytes)
        {
            string response = string.Empty;

            foreach (byte b in bytes)
                response += (char)b;

            return response;
        }
    }
}