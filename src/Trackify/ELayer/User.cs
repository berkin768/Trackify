using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Trackify
{
    public class User
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public Date DateOfBirth { get; set; }
        public string RealName { get; set; }
        public string RealSurname { get; set; }
        public string UserName { get; set; }
        public int FollowerCount { get; set; }
        public string PassHash { get; set; }


        public bool Validate(string Pass)
        {
            byte[] hash;
            using (MD5 md5 = MD5.Create())
            {
                hash = md5.ComputeHash(Encoding.UTF8.GetBytes(Pass));
            }
            byte[] tmpRealHash = ASCIIEncoding.ASCII.GetBytes(PassHash);
            if(!(tmpRealHash.Length == hash.Length))
            {
                return false;
            }
            for(int i = 0; i < tmpRealHash.Length; i++)
            {
                if(tmpRealHash[i] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
        public void SetUserPass(string NewPass)
        {
            byte[] tmpHash;
            using (MD5 md5 = MD5.Create())
            {
                tmpHash = md5.ComputeHash(Encoding.UTF8.GetBytes(NewPass));
            }
            StringBuilder sOutput = new StringBuilder(tmpHash.Length);//Convert this last ByteArray to string
            for (int i = 0; i < tmpHash.Length; i++)
            {
                sOutput.Append(tmpHash[i].ToString("X2"));
            }
            this.PassHash = sOutput.ToString();//Convert this last ByteArray to string
            return;
        }
    }
}