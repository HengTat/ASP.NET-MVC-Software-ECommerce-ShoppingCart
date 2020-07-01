using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart_9b.Models
{
    public class ActivationCode
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string OrderDetailsId { get; set; }
        public virtual Product Product { get; set; }
        public virtual OrderDetails OrderDetails { get; set; }
    }
}


