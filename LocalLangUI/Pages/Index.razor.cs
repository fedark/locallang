using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalLangUI.Resources;
using LocalLangLibrary.Models;
using LocalLangUI.Helpers;

namespace LocalLangUI.Pages
{
	public partial class Index
    {
        private IList<Category>? categories_;
        private IList<Expression>? expressions_;

        private string category_ = LabelResource.CategoryAll;
        private string? searchText_;
        private SortType sortType_ = SortType.Alphabet;
        private bool showCategories_ = false;

        #region Initialization

        protected async override Task OnInitializedAsync()
        {
            categories_ = await dbCategories.GetAllAsync();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadFilterStateAsync();
                await FilterExpressionsAsync();
                StateHasChanged();
            }
        }

        private async Task LoadFilterStateAsync()
        {
            var stringResults = await sessionStorage.GetAsync<string>(nameof(category_));
            category_ = stringResults.Success && stringResults.Value is not null ? stringResults.Value : LabelResource.CategoryAll;

            stringResults = await sessionStorage.GetAsync<string>(nameof(searchText_));
            searchText_ = stringResults.Success && stringResults.Value is not null ? stringResults.Value : string.Empty;

            var sortResults = await sessionStorage.GetAsync<SortType>(nameof(sortType_));
            sortType_ = sortResults.Success ? sortResults.Value : SortType.Alphabet;
        }

        private async Task FilterExpressionsAsync()
        {
            IEnumerable<Expression>? result = await dbExpressions.GetAllAsync();
            if (category_ != LabelResource.CategoryAll)
            {
                result = result?.Where(e => e.Category.Name == category_);
            }

            if (!string.IsNullOrWhiteSpace(searchText_))
            {
                result = result?.Where(e => e.Word.Contains(searchText_, StringComparison.InvariantCultureIgnoreCase) ||
                    e.Translation.Contains(searchText_, StringComparison.InvariantCultureIgnoreCase));
            }

            result = sortType_ switch
            {
                SortType.Alphabet => result?.OrderBy(e => e.Word),
                SortType.New => result?.OrderByDescending(e => e.DateCreated),
                SortType.Popular => result?.OrderByDescending(e => e.Likes),
                _ => result
            };

            expressions_ = result?.ToList();
            await SaveFilterStateAsync();
        }

        private async Task SaveFilterStateAsync()
        {
            await sessionStorage.SetAsync(nameof(category_), category_);
            await sessionStorage.SetAsync(nameof(searchText_), searchText_ ?? string.Empty);
            await sessionStorage.SetAsync(nameof(sortType_), sortType_);
        }

        #endregion

        #region Navigation

        private void OpenCreate()
        {
            navManager.NavigateTo("/Create");
        }

        private void OpenDetails(Expression expression)
        {
            navManager.NavigateTo($"/Details/{expression.Id}");
        }

        #endregion

        #region Events

        private async Task OnSortAsync(SortType sorting)
        {
            sortType_ = sorting;
            await FilterExpressionsAsync();
        }

        private async Task OnSearchInputAsync(string? searchText)
        {
            searchText_ = searchText;
            await FilterExpressionsAsync();
        }

        private async Task OnCategoryClickAsync(string category)
        {
            category_ = category;
            showCategories_ = false;
            await FilterExpressionsAsync();
        }

        private async Task LikeAsync(Expression expression)
        {
            await dbExpressions.LikeAsync(expression);
            await FilterExpressionsAsync();
        }

        #endregion

        #region Markup

        private string GetLikeTopText(Expression expression)
        {
            return expression.Likes > 0 ? expression.Likes.ToString() : LabelResource.LikeTopText;
        }

        private string GetLikeBottomText(Expression expression)
        {
            if (expression.Likes < 1)
            {
                return string.Empty;
            }

            return PluralHelper.GetCountedText(expression.Likes, LabelResource.LikeBottomText, LabelResource.LikeBottomTextPlural2to4, LabelResource.LikeBottomTextPlural);
        }

        private string GetExpressionCountText()
        {
            var count = expressions_?.Count ?? 0;
            return PluralHelper.GetCountedText(count, LabelResource.Expression, LabelResource.ExpressionPlural2to4, LabelResource.ExpressionPlural);
        }

        private string GetSortClass(bool isSelected)
        {
            return isSelected ? "order-selected" : string.Empty;
        }

        private string GetLikeClass(Expression expression)
        {
            return expression.Likes > 0 ? "expression-entry-liked" : "expression-entry-no-likes";
        }

        private string GetSelectedCategoryClass(string category)
        {
            return category == category_ ? "selected-category" : string.Empty;
        }

        #endregion

        #region Sort Type Class

        private enum SortType
        {
            Alphabet,
            New,
            Popular
        }

		#endregion
	}
}