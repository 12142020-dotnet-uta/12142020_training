using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ModelLayer.ViewModels
{
	public class PlayerViewModel
	{
		//[Key]
		public Guid playerId { get; set; } = Guid.NewGuid();

		[StringLength(20, ErrorMessage = "The first name must be from 3 to 20 characters.", MinimumLength = 3)]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
		[Required]
		[Display(Name = "First Name")]
		public string Fname { get; set; }

		[StringLength(20, ErrorMessage = "The last name must be from 3 to 20 characters.", MinimumLength = 3)]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
		[Required]
		[Display(Name = "First Name")]
		public string Lname { get; set; }

		[Range(0, int.MaxValue)]
		[Display(Name = "Number of wins")]
		public int numWins { get; set; }

		[Range(0, int.MaxValue)]
		[Display(Name = "Number of losses")]
		public int numLosses { get; set; }

		public IFormFile IformFileImage { get; set; }
		public string JpgStringImage { get; set; }
	}
}
