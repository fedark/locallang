using System.ComponentModel.DataAnnotations;

namespace LocalLangUI.Models
{
	public class CreateExpressionModel
	{
		[Required]
		[MaxLength(75)]
		public string Expression { get; set; }

		[Required]
		[MinLength(1)]
		[Display(Name = "Category")]
		public string CategoryId { get; set; }

		[MaxLength(500)]
		public string Description { get; set; }
	}
}
