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
            var fromBaseCity = Convert.FromBase64String(user.City);
            var city = dataCypher.DecryptStringFromBytes_Aes(
                fromBaseCity.Take(fromBaseCity.Length - 16).ToArray(),
            Encoding.ASCII.GetBytes(key),
            cityIV
                );
            Console.WriteLine(city);
            var fromBaseMobile = Convert.FromBase64String(user.MobilePhone) ;
            var mobile = dataCypher.DecryptStringFromBytes_Aes(
                fromBaseMobile.Take(fromBaseMobile.Length-16).ToArray(),
                Encoding.ASCII.GetBytes(key),
                mobileIv
            );
            return new User(user.Username, user.Password, mobile, city);
        }
    }
}