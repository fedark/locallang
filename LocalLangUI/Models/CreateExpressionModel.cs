using System.ComponentModel.DataAnnotations;

namespace LocalLangUI.Models
{
	public class CreateExpressionModel
	{
		[Required]
		[MaxLength(75)]
		public string Word { get; set; }

		[MaxLength(500)]
		public string Translation { get; set; }

		[Required]
		[MinLength(1)]
		[Display(Name = "Category")]
		public string Category { get; set; }
	}
}
