using System.Collections.Generic;
using System.Threading.Tasks;
using LocalLangLibrary.Models;
using LocalLangUI.Helpers;
using LocalLangUI.Resources;

namespace LocalLangUI.Pages
{
	public partial class Admin
	{
		private IList<Expression>? submissions_;

		private string editedExpressionId_ = string.Empty;
		private bool isWordEdited_ = false;
		private bool isTranslationEdited_ = false;
		private string editedWord_ = string.Empty;
		private string editedTranslation_ = string.Empty;

		protected override async Task OnInitializedAsync()
		{
			submissions_ = await dbExpressions.GetPendingAsync();
		}

		private void ClosePage()
		{
			navManager.NavigateTo("/");
		}

		private async Task ApproveAsync(Expression submission)
		{
			submission.Status = Status.Approved;
			submissions_?.Remove(submission);
			await dbExpressions.UpdateAsync(submission);
		}

		private async Task RejectAsync(Expression submission)
		{
			submission.Status = Status.Rejected;
			submissions_?.Remove(submission);
			await dbExpressions.UpdateAsync(submission);
		}

		private void EditWord(Expression expression)
		{
			editedExpressionId_ = expression.Id;
			isWordEdited_ = true;
			isTranslationEdited_ = false;
			editedWord_ = expression.Word;
		}

		private void EditTranslation(Expression expression)
		{
			editedExpressionId_ = expression.Id;
			isWordEdited_ = false;
			isTranslationEdited_ = true;
			editedTranslation_ = expression.Translation;
		}

		private async Task SaveAsync(Expression expression)
		{
			if (isWordEdited_)
			{
				isWordEdited_ = false;
				expression.Word = editedWord_;
			}

			if (isTranslationEdited_)
			{
				isTranslationEdited_ = false;
				expression.Translation = editedTranslation_;
			}

			await dbExpressions.UpdateAsync(expression);
		}

		private void Cancel()
		{
			editedExpressionId_ = string.Empty;
		}

		private string GetSubmissionCountText()
		{
			var count = submissions_?.Count ?? 0;
			return PluralHelper.GetCountedText(count, LabelResource.Submission, LabelResource.SubmissionPlural2to4, LabelResource.SubmissionPlural);
		}
	}
}