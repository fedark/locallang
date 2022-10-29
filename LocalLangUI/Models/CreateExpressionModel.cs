using System.ComponentModel.DataAnnotations;
using LocalLangUI.Resources;

namespace LocalLangUI.Models
{
	public class CreateExpressionModel
	{
		[Required(ErrorMessageResourceName = nameof(ErrorMessageResource.ExpressionRequiredError), ErrorMessageResourceType = typeof(ErrorMessageResource))]
		[MaxLength(75, ErrorMessageResourceName = nameof(ErrorMessageResource.ExpressionInvalidError), ErrorMessageResourceType = typeof(ErrorMessageResource))]
		public string? Word { get; set; }

		[Required(ErrorMessageResourceName = nameof(ErrorMessageResource.TranslationRequiredError), ErrorMessageResourceType = typeof(ErrorMessageResource))]
		[MaxLength(500, ErrorMessageResourceName = nameof(ErrorMessageResource.TranslationInvalidError), ErrorMessageResourceType = typeof(ErrorMessageResource))]
		public string? Translation { get; set; }

		[Required(ErrorMessageResourceName = nameof(ErrorMessageResource.CategoryRequiredError), ErrorMessageResourceType = typeof(ErrorMessageResource))]
		[MinLength(1, ErrorMessageResourceName = nameof(ErrorMessageResource.CategoryInvalidError), ErrorMessageResourceType = typeof(ErrorMessageResource))]
		[Display(Name = "Category")]
		public string? Category { get; set; }
	}
}
