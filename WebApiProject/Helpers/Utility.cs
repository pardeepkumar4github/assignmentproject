using WebApiProject.Models;

namespace WebApiProject.Helper
{
    public static class Utility
    {
        public static decimal GetDistance(Address sourceAddress, Address destinationAddress)
        {
            // We are returning this hard coded value here.
            // But, in actual application we can calculate the actual distance using some third party api,
            // like google maps based on the provided source and destination addresses.
            return 250; // This is test distance given in kilometers. It can also be in any other units as per requirement in real scenario.
        }
    }
}
