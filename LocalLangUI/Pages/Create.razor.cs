using System.Collections.Generic;
using System.Threading.Tasks;
using LocalLangLibrary.Models;
using LocalLangUI.Models;

namespace LocalLangUI.Pages
{
	public partial class Create
	{
		private CreateExpressionModel newExpression_ = new();
		private IList<Category>? categories_;

		protected override async Task OnInitializedAsync()
		{
			categories_ = await dbCategories.GetAllAsync();
		}

		private void ClosePage()
		{
			navManager.NavigateTo("/");
		}

		private async Task CreateExpressionAsync()
		{
			var category = await dbCategories.GetAsync(newExpression_.Category);
			var expression = new Expression(newExpression_.Word, newExpression_.Translation, category!);
			await dbExpressions.CreateAsync(expression);
			newExpression_ = new();
			ClosePage();
		}
	}
}