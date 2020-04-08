using UnityEngine;
using HiddenObject.Components;
using HiddenObject.UI.Base;

namespace HiddenObject.UI.Windows
{
	public class LevelCompletePopupView : BaseView
	{
		#region SERIALIZE FIELDS

		[SerializeField]
		private StarComponent _starComponent;
		[SerializeField]
		private ButtonLabel _replyButton;
		[SerializeField]
		private ButtonLabel _closeButton;

		#endregion

		#region PUBLIC PROPERTIES

		public StarComponent StarComponent => _starComponent;

		public ButtonLabel ReplyButton => _replyButton;

		public ButtonLabel CloseButton => _closeButton;

		#endregion
	}
}