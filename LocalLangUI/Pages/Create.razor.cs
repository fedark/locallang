using System.Collections.Generic;
using System.Threading.Tasks;
using LocalLangUI.Models;
using LocalLangLibrary.Models;

namespace LocalLangUI.Pages
{
	public partial class Create
    {
        private CreateExpressionModel newExpression_ = new();
        private IList<Category>? categories_;

        protected async override Task OnInitializedAsync()
        {
            categories_ = await dbCategories.GetAllAsync();
        }

        private void ClosePage()
        {
            navManager.NavigateTo("/");
        }

        private async Task CreateExpression()
        {
#nullable disable
            var category = await dbCategories.GetAsync(newExpression_.Category);
            var expression = new Expression(newExpression_.Word, newExpression_.Translation, category);
#nullable restore
            await dbExpressions.Create(expression);
            newExpression_ = new();
            ClosePage();
        }
    }
}