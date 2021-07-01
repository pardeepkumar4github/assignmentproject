namespace WebApiProject.Models
{
    public class FedexCourierInputData
    {
        public Address Consignee { get; set; }
        public Address Consignor { get; set; }
        public decimal[] Carton { get; set; }
    }
}
