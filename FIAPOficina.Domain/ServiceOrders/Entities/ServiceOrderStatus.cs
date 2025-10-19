using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Domain.ServiceOrders.Entities
{
    public enum ServiceOrderStatus
    {
        Received = 0,
        InDiagnosis = 1,
        WaitingApproval = 2,
        Running = 3,
        Completed = 4,
        Delivered = 5
    }
}
