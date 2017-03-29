using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required"), MaxLength(30)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Name User is required"), MaxLength(30)]

        public string User_Name { get; set; }
        [Required(ErrorMessage = "Password is required"), MaxLength(30)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email is required"), MaxLength(40),EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mobile Number is required"), MaxLength(11),RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Location is required"), MaxLength(500)]
        public string Location { get; set; }

        public int IsAdmin { get; set; }
        public int cartItem { get; set; }
        public int purchasedItem { get; set; }

        public List<UserProduct> UserProducts { get; set; }
        public List<Cart> Carts { get; set; }
    }
}
