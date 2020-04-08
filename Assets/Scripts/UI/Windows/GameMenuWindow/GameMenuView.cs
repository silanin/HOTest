using UnityEngine;
using UnityEngine.UI;
using HiddenObject.Components;
using HiddenObject.UI.Base;

namespace HiddenObject.UI.Windows
{
	public class GameMenuView : BaseView
	{
		#region SERIALIZE FIELDS

		[SerializeField]
		private LevelItemView _levelItemView;
		[SerializeField]
		private ButtonLabel _playButton;
		[SerializeField]
		private Toggle _nightModeToggle;

		#endregion

		#region PUBLIC PROPERTIES

		public LevelItemView LevelItemView => _levelItemView;

		public ButtonLabel PlayButton => _playButton;

		public Toggle NightModeToggle => _nightModeToggle;

        #endregion
	}
}