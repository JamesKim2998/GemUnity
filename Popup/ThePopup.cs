using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gem
{
	[Flags]
	public enum PopupMode
	{
		NONE = 0,
		CLOSE_ON_BG,
	}

	public static class ThePopup
	{
		private struct PopupData
		{
			public PopupData(Popup _popup, PopupMode _mode)
			{
				popup = _popup;
				mode = _mode;
			}

			public readonly Popup popup;
			public readonly PopupMode mode;
		}

		private static Canvas sCanvas;
		public static Canvas canvas
		{
			get { return sCanvas ?? (sCanvas = UnityEngine.Object.FindObjectOfType<Canvas>()); }
		}

		public static bool isOpened { get { return sCurrent.popup != null; } }

		// todo: lock 없이 재진입 가능하도록 수정.
		public static bool isLocked { get; private set; }

		// todo: Current가 삭제되었을 경우에도 작동하도록.
		private static PopupData sCurrent = new PopupData(null, PopupMode.NONE);
		private static readonly List<PopupData> sStash = new List<PopupData>();

		public static Popup Current()
		{
			return sCurrent.popup;
		}

		public static Popup Open(PopupKey _key, PopupMode _mode = PopupMode.NONE, object _arg = null)
		{
			if (isLocked)
			{
				L.W("locked.");
				return null;
			}

			var _popup = PopupDB.Create(_key, _arg);
			if (_popup == null) return null;

			isLocked = true;

			var _old = sCurrent.popup;
			if (_old)
			{
				_old.ComeToBG();

				if (sCurrent.mode.Has(PopupMode.CLOSE_ON_BG))
				{
					_old.Close();
					sStash.RemoveBack();
				}
				else
				{
					sStash.Add(sCurrent);
				}
			}

			sCurrent = new PopupData(_popup, _mode);

			((RectTransform)_popup.transform).SetParentIdentity((RectTransform)canvas.transform);

			_popup.Open(_arg);

			isLocked = false;

			return _popup;
		}

		public static bool Close(int _instanceID)
		{
			if (!isOpened)
			{
				L.W("trying to close but popup is not opened.");
				return false;
			}

			if (isLocked)
			{
				L.W("locked.");
				return false;
			}

			if (Current().GetInstanceID() == _instanceID)
			{
				Close();
				return true;
			}
			else
			{
				var i = -1;

				foreach (var _data in sStash)
				{
					++i;
					if (_data.popup == null)
						continue;
					if (_data.popup.GetInstanceID() == _instanceID)
					{
						_data.popup.Close();
						sStash.RemoveAt(i);
						return true;
					}
				}
			}

			return false;
		}

		public static bool Close()
		{
			if (!isOpened)
			{
				L.W("trying to close but popup is not opened.");
				return false;
			}

			if (isLocked)
			{
				L.W("locked.");
				return false;
			}

			sCurrent.popup.Close();
			sCurrent = new PopupData(null, PopupMode.NONE);

			while (!sStash.Empty())
			{
				var _data = sStash.Last();
				if (_data.popup == null)
				{
					sStash.RemoveBack();
					continue;
				}

				sCurrent = _data;
				sCurrent.popup.ComeToFG();
				break;
			}

			return true;
		}

		public static void Clear()
		{
			if (!isOpened)
				return;
			
			sCurrent.popup.Close();

			foreach (var _data in sStash.GetReverseEnum())
				_data.popup.Close();

			sStash.Clear();
		}
	}
}
