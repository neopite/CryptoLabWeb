﻿using System;
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
            var cityIV = Encoding.UTF8.GetBytes(user.City).TakeLast(16).ToArray();
            var mobileIv = Encoding.UTF8.GetBytes(user.MobilePhone).TakeLast(16).ToArray();
            var dataCypher = new DataCypherSolver();
            var city =   dataCypher.DecryptStringFromBytes_Aes(Convert.FromBase64String(user.City),
                Encoding.ASCII.GetBytes(key),
                cityIV
            );
            
            var mobile = dataCypher.DecryptStringFromBytes_Aes(Convert.FromBase64String(user.MobilePhone),
                Encoding.ASCII.GetBytes(key),
                mobileIv
            );
            return new User(user.Username,user.Password,mobile,city);
        }
        
    }
    
}