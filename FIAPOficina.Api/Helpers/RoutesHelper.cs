namespace FIAPOficina.Api.Helpers
{
    public static class RoutesHelper
    {
        public static class Auth
        {
            public const string Controller = "/api/v1/auth";
            public const string Login = "/api/v1/auth/login";
        }

        public static class Users
        {
            public const string Controller = "/api/v1/users";
            public const string Create = "/api/v1/users";
            public const string Update = "/api/v1/users/{id}";
            public const string Delete = "/api/v1/users/{id}";
            public const string GetSingle = "/api/v1/users/{id}";
            public const string GetAll = "/api/v1/users";
        }

        public static class Clients
        {
            public const string Controller = "/api/v1/clients";
            public const string Create = "/api/v1/clients";
            public const string Update = "/api/v1/clients/{id}";
            public const string Delete = "/api/v1/clients/{id}";
            public const string GetSingle = "/api/v1/clients/{id}";
            public const string GetAll = "/api/v1/clients";
        }

        public static class Vehicles
        {
            public const string Controller = "/api/v1/vehicles";
            public const string Create = "/api/v1/vehicles";
            public const string Update = "/api/v1/vehicles/{id}";
            public const string Delete = "/api/v1/vehicles/{id}";
            public const string GetSingle = "/api/v1/vehicles/{id}";
            public const string GetAll = "/api/v1/vehicles";
        }

        public static class Services
        {
            public const string Controller = "/api/v1/services";
            public const string Create = "/api/v1/services";
            public const string Update = "/api/v1/services/{id}";
            public const string Delete = "/api/v1/services/{id}";
            public const string GetSingle = "/api/v1/services/{id}";
            public const string GetAll = "/api/v1/services";
        }

        public static class Materials
        {
            public const string Controller = "/api/v1/materials";
            public const string Create = "/api/v1/materials";
            public const string Update = "/api/v1/materials/{id}";
            public const string Delete = "/api/v1/materials/{id}";
            public const string GetSingle = "/api/v1/materials/{id}";
            public const string GetAll = "/api/v1/materials";
        }

        public static class ServiceOrders
        {
            public const string Controller = "/api/v1/service-orders";
            public const string Create = "/api/v1/service-orders";
            public const string Update = "/api/v1/service-orders/{id}";
            public const string Delete = "/api/v1/service-orders/{id}";
            public const string GetSingle = "/api/v1/service-orders/{id}";
            public const string GetAll = "/api/v1/service-orders";
            public const string StartServiceOrderDiagnosis = "/api/v1/service-orders/{id}/start-diagnosis";
            public const string RequestServiceOrderApproval = "/api/v1/service-orders/{id}/request-approval";
            public const string ApproveServiceOrder = "/api/v1/service-orders/{id}/approve";
            public const string RejectServiceOrder = "/api/v1/service-orders/{id}/reject";
            public const string StartServiceOrder = "/api/v1/service-orders/{id}/start";
            public const string CompleteServiceOrder = "/api/v1/service-orders/{id}/complete";
            public const string DeliverServiceOrder = "/api/v1/service-orders/{id}/deliver";
            public const string GetClientVehicleServiceOrders = "/api/v1/service-orders/client{clientIdentifier}/vehicle{vehiclePlate}";
            public const string GetAverage = "/api/v1/service-orders/average";            
        }
    }
}
