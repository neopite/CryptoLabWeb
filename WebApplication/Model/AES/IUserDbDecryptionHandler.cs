using System;
using System.Linq;
using System.Text;

namespace WebApplication.Model.AES
{
    public interface IUserDbDecryptionHandler
    {
        public User DecryptUserInfoFromDb(User user, string key);
    }

    public class UserDbDecryptionHandler : IUserDbDecryptionHandler
    {
        public User DecryptUserInfoFromDb(User user, string key)
        {
            var dataCypher = new DataCypherSolver();
            var fromBaseCity = Convert.FromBase64String(user.City);
            var fromBaseMobile =  Convert.FromBase64String(user.City);
            var decryptedCity = dataCypher.Decrypt(fromBaseCity, key);
            var decryptedMobile = dataCypher.Decrypt(fromBaseMobile, key);
          
            return new User(user.Username, user.Password, decryptedMobile, decryptedCity);
        }
    }
}