namespace FIAPOficina.Api.Helpers
{
    public static class RoutesHelper
    {
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
        }
    }
}
