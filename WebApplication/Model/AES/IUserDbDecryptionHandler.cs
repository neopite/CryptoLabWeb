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
            var cityIV = Convert.FromBase64String(user.City).TakeLast(16).ToArray();
            var mobileIv = Convert.FromBase64String(user.MobilePhone).TakeLast(16).ToArray();
            var dataCypher = new DataCypherSolver();
            Console.WriteLine(user.City);
            var city = dataCypher.DecryptStringFromBytes_Aes(
                Convert.FromBase64String(user.City),
            Encoding.ASCII.GetBytes(key),
            cityIV
                );
            city = city.Substring(0, city.Length - 16);
            var mobile = dataCypher.DecryptStringFromBytes_Aes(
                Convert.FromBase64String(user.MobilePhone),
                Encoding.ASCII.GetBytes(key),
                mobileIv
            );
            mobile = mobile.Substring(0, mobile.Length - 16);
            return new User(user.Username, user.Password, mobile, city);
        }
    }
}