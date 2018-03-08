using System;
using System.Security.Cryptography;

namespace Service.Tools
{
    public class Hashing
    {
        public string Hash { get; set; } = null;
        public string Salt { get; set; } = null;

        public Hashing(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, 20))
            {
                this.Hash = Convert.ToBase64String(deriveBytes.GetBytes(20));
                this.Salt = Convert.ToBase64String(deriveBytes.Salt);
            }
        }

        public Hashing(string password, string salt)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt)))
            {
                this.Hash = Convert.ToBase64String(deriveBytes.GetBytes(20));
                this.Salt = Convert.ToBase64String(deriveBytes.Salt);
            }
        }

        public bool Equals(string dbHash)
        {
            return this.Hash == dbHash;
        }
    }
}