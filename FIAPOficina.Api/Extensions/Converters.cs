using FIAPOficina.Api.Models.ServiceOrders.Requests;
using FIAPOficina.Api.Models.ServiceOrders.Responses;
using FIAPOficina.Application.ServiceOrders.Commands.CreateServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.UpdateServiceOrder;
using FIAPOficina.Domain.ServiceOrders.Entities;

namespace FIAPOficina.Api.Extensions
{
    public static class Converters
    {
        public static ServiceOrderMaterialResponse ToResponse(this ServiceOrderMaterial model)
        {
            return new(
                Id: model.Id,
                MaterialId: model.MaterialId,
                Quantity: model.Quantity,
                ServiceOrderId: model.ServiceOrderId,
                UnitValue: model.UnitValue
            );
        }

        public static ServiceOrderServiceResponse ToResponse(this ServiceOrderService model)
        {
            return new(
                Id: model.Id,
                ServiceId: model.ServiceId,
                Quantity: model.Quantity,
                ServiceOrderId: model.ServiceOrderId,
                UnitValue: model.UnitValue
            );
        }

        public static ServiceOrderMaterialToCreate ToCreationModel(this ServiceOrderMaterialRequest request, Guid? serviceOrderId = null)
        {
            return new(
                materialId: request.MaterialId,
                quantity: request.Quantity
            );
        }

        public static ServiceOrderServiceToCreate ToCreationModel(this ServiceOrderServiceRequest request, Guid? serviceOrderId = null)
        {
            return new(
                serviceId: request.ServiceId,
                quantity: request.Quantity
            );
        }

        public static ServiceOrderMaterialToUpdate ToUpdateModel(this ServiceOrderMaterialRequest request, Guid? serviceOrderId = null)
        {
            return new(
                request.Id,
                materialId: request.MaterialId,
                quantity: request.Quantity
            );
        }

        public static ServiceOrderServiceToUpdate ToUpdateModel(this ServiceOrderServiceRequest request, Guid? serviceOrderId = null)
        {
            return new(
                request.Id,
                serviceId: request.ServiceId,
                quantity: request.Quantity
            );
        }
    }
}
