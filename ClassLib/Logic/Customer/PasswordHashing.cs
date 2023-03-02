using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace ClassLib.Logic
{
    public class PasswordHashing
    {
        public string passwordHash256(string password)
        {
            string hex = "";
            foreach (byte i in new SHA256Managed().ComputeHash(Encoding.ASCII.GetBytes(password)))
            {
                hex += string.Format("{0:x2}", i);
            }
            return hex.ToUpper();
        }
    }
}
