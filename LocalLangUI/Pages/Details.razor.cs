using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using LocalLangUI.Resources;
using LocalLangLibrary.Models;
using LocalLangUI.Helpers;

namespace LocalLangUI.Pages
{
	public partial class Details
    {
        [Parameter]
        [NotNull]
        public string? Id { get; set; }

        private Expression? expression_;

        protected async override Task OnInitializedAsync()
        {
            expression_ = await dbExpressions.GetAsync(Id);
        }

        private void ClosePage()
        {
            navManager.NavigateTo("/");
        }

        private async Task LikeAsync()
        {
            if (expression_ is not null)
            {
                await dbExpressions.LikeAsync(expression_);
            }
        }

        private string GetLikeTopText()
        {
            return expression_?.Likes > 0 ? expression_.Likes.ToString() : LabelResource.LikeTopText;
        }

        private string GetLikeBottomText()
        {
            if (expression_ is null || expression_.Likes < 1)
            {
                return string.Empty;
            }

            return PluralHelper.GetCountedText(expression_.Likes, LabelResource.LikeBottomText, LabelResource.LikeBottomTextPlural2to4, LabelResource.LikeBottomTextPlural);
        }

        private string GetLikeClass()
        {
            return expression_?.Likes > 0 ? "expression-detail-liked" : "expression-detail-no-likes";
        }
    }
}