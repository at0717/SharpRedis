using System;

namespace SharpRedis.ConsoleApp
{
    public class Account
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string CreditCardNumber { get; set; }
        public int Level { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public DateTime LoginTime { get; set; }
        public long Sequence { get; set; }
    }
}