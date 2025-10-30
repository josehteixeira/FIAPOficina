namespace FIAPOficina.Api.Models.ServiceOrders.Responses
{
    public record ServiceOrderResponse
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public ServiceOrderServiceResponse[] Services { get; set; }
        public ServiceOrderMaterialResponse[] Materials { get; set; }
        public int Status { get; set; }
    }
}