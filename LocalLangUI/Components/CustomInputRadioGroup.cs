using Microsoft.AspNetCore.Components.Forms;

namespace LocalLangUI.Components
{
	public class CustomInputRadioGroup<TValue> : InputRadioGroup<TValue>
	{
		private string fieldClass_;
		private string name_;

		protected override void OnParametersSet()
		{
			var fieldClass = EditContext?.FieldCssClass(FieldIdentifier) ?? string.Empty;
			if (fieldClass != fieldClass_ || Name != name_)
			{
				fieldClass_ = fieldClass;
				name_ = Name;
				base.OnParametersSet();
			}
		}
	}
}
