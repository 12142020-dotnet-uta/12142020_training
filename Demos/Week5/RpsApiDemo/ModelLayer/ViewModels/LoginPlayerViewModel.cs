using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
	public class LoginPlayerViewModel
	{
		[StringLength(20, ErrorMessage = "The first name must be from 3 to 20 characters.", MinimumLength = 3)]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
		[Required]
		[Display(Name = "First Name")]
		public string Fname { get; set; }

		[StringLength(20, ErrorMessage = "The last name must be from 3 to 20 characters.", MinimumLength = 3)]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
		[Required]
		[Display(Name = "Last Name")]
		[DataType(DataType.Password)]
		public string Lname { get; set; }
	}
}
