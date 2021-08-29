using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DbOper
{
    public class User
    {
        [Required(ErrorMessage ="Please enter User ID")]
        [Display(Name ="User ID")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter User Name")]
        [MaxLength(50)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter User Address")]
        [Display(Name = "User Address")]
        [MaxLength(50)]
        public string Address { get; set; }
    }
}
