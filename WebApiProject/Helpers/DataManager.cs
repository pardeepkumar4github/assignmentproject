using System.Collections.Generic;
using System.Linq;
using WebApiProject.Models;

namespace WebApiProject.Helper
{
    // Here we are keeping dummy data. It should be stored in db or in any other data sourcce in real applications.
    public class DataManager
    {
        public static List<User> GetUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1000,
                    ApplicationId = 1,
                    FirstName = "testFirstName01",
                    LastName = "testLastName01",
                    Username = "Test_EcomExpress_Username",
                    Password = "Test_EcomExpress_Password"
                },
                new User
                {
                    Id = 1111,
                    ApplicationId = 2,
                    FirstName = "testFirstName02",
                    LastName = "testLastName02",
                    Username = "Test_FedexCourier_Username",
                    Password = "Test_FedexCourier_Password"
                },
                new User
                {
                    Id = 1234,
                    ApplicationId = 3,
                    FirstName = "testFirstName02",
                    LastName = "testLastName02",
                    Username = "Test_SafeExpress_Username",
                    Password = "Test_SafeExpress_Password"
                },
            };
        }

        public static Dictionary<string, decimal> Prices = new Dictionary<string, decimal>()
        {
            // Here we are hard coding these prices for different companies.
            // But in actual application, these values can be stored in database and can be fetched from there.
            // Here, we are taking these prices per cubic centimeter and per 100 kilometers.
            // The measurement units can be different in actual application. The measuement units can also be saved in database.
            // In the same way, the prices mentioned here for different companies can also be saved in db and their units too; i.e. INR, Dollar etc.
            // These prices are for per 100 kilometers. This can also be saved in db in real scenario.
            {"EcomExpress", 2 },
            {"FedexCourier", 1.5m },
            {"SafeExpress", 3 }
        };

        public static decimal GetTestData(string companyName)
        {
            return Prices.FirstOrDefault(x => x.Key == companyName).Value;
        }
    }
}
