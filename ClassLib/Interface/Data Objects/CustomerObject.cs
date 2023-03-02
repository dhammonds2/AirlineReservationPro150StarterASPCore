using System;

namespace ClassLib.Interface
{
    public class CustomerObject
    {
        public int customerId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string gender { get; set; }
        public string password { get; set; }
        public bool isAdmin { get; set; }
    }
}
