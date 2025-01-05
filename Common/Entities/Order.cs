using Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Order:BaseEntity
    {
        public OrderStatus Status { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Guid PaymentToken {  get; set; }
        public ICollection <OrderProduct> OrderProducts { get; set; }
    }
}
