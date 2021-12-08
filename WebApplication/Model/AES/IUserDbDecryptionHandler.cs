namespace WebApplication.Model.AES
{
    public interface IUserDbDecryptionHandler
    {
        public User DecryptUserInfoFromDb(User user, byte[] IV, string key);
    }

    public class UserDbDecryptionHandler : IUserDbDecryptionHandler
    {
        public User DecryptUserInfoFromDb(User user, byte[] IV, string key)
        {
            throw new System.NotImplementedException();
        }
    }
    
}