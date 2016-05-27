using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Faker;
using SharpRedis.Jil;

namespace SharpRedis.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Start..");

            var config = new Config();
            config.SetSerializerFactory(new SerializerFactory());

            var list = Client.GetList<Account>(8000, 20, true).ToList();

            //ConnectionMultiplexer conn = ConnectionMultiplexer.Connect("127.0.0.1");
            //var subscriber = conn.GetSubscriber();

            //subscriber.Subscribe("message", (channel, message) => Console.WriteLine(message));

            var account = new Account
            {
                Id = Guid.NewGuid(),
                UserName = Name.GetName(),
                Address = Address.GetStreetAddress(),
                CreditCardNumber = CreditCard.CreditCardNumber("VISA"),
                Company = Company.GetName(),
                Level = 1,
                LoginTime = DateTime.Now,
                Mobile = PhoneNumber.GetPhoneNumber(),
                Sequence = 1
            };

            var user = new User {ID = "ddd"};

            Client.Set(account);
            //FakeData();

            GetList();

            Console.WriteLine("Done.");

            Console.ReadKey();
        }

        private static void GetList()
        {
            var sw = new Stopwatch();

            sw.Start();

            var list = Client.GetList<Account>(8000, 20, true).ToList();
            sw.Stop();

            Console.WriteLine("get list done.\r\n cost: {0}ms", sw.ElapsedMilliseconds);

            foreach (var account in list)
            {
                Console.WriteLine("Id:{0}, Name{1}", account.Id, account.UserName);
            }
        }

        private static void FakeData()
        {
            var accounts = new List<Account>();

            for (int i = 1; i < 100001; i++)
            {
                var account = new Account
                {
                    Id = Guid.NewGuid(),
                    UserName = Name.GetName(),
                    Address = Address.GetStreetAddress(),
                    CreditCardNumber = CreditCard.CreditCardNumber("VISA"),
                    Company = Company.GetName(),
                    Level = 1,
                    LoginTime = DateTime.Now,
                    Mobile = PhoneNumber.GetPhoneNumber(),
                    Sequence = i
                };

                accounts.Add(account);
            }

            var sw = new Stopwatch();

            sw.Start();

            Client.SetList(accounts.ToArray(), x=> x.Id, x=>x.Sequence);

            sw.Stop();

            Console.WriteLine("insert data done.\r\n cost: {0}ms", sw.ElapsedMilliseconds);
        }
    }
}