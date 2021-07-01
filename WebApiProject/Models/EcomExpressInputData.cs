namespace WebApiProject.Models
{
    public class EcomExpressInputData
    {
        public Address ContactAddress { get; set; }
        public Address WarehouseAddress { get; set; }
        public decimal[] PackageDimensions { get; set; }
    }
}
