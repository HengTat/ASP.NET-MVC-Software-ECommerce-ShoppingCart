
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart_9b.Models
{
    public class CartDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        public string CartId { get; set; }
        [Required]
        public string ProductId { get; set; }
        public int Qty { get; set; }

        public virtual Product Product {get; set;}
    }
}




