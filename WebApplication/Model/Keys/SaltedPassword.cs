namespace WebApplication.Model.Keys
{
    public class SaltedPassword
    {
        public string Hash { get; private set; }
        public string Salt { get; private set; }

        public SaltedPassword(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }
    }
}