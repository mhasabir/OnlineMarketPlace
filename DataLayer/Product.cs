using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; }

        [MaxLength(100)]
        public string Picture { get; set; }

        [Required(ErrorMessage = "Product Details is required"), MaxLength(1000)]
        public string Details { get; set; }

        [Required(ErrorMessage = "Every Product should have a Category")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Every Product should have a Price")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Quantity Required")]
        public int Quantity { get; set; }

        public int sold { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public List<UserProduct> UserProducts { get; set; }
        public List<Cart> Carts { get; set; }
    }
}
