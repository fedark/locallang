using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using LocalLangUI.Resources;
using LocalLangLibrary.Models;

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

        private async Task Like()
        {
            if (expression_ is not null)
            {
                expression_.Likes++;
                await dbExpressions.UpdateAsync(expression_);
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

            return (expression_.Likes % 100, expression_.Likes % 10) switch
            {
                (1, _) => LabelResource.LikeBottomText,
                ( >= 2 and <= 4, _) => LabelResource.LikeBottomTextPlural2to4,
                ( > 4 and <= 20, _) => LabelResource.LikeBottomTextPlural,
                (_, 1) => LabelResource.LikeBottomText,
                (_, >= 2 and <= 4) => LabelResource.LikeBottomTextPlural2to4,
                _ => LabelResource.LikeBottomTextPlural,
            };
        }

        private string GetLikeClass()
        {
            return expression_?.Likes > 0 ? "expression-detail-liked" : "expression-detail-no-likes";
        }
    }
}