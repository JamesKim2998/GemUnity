using System;
using System.Collections.Generic;

namespace Gem
{
	using Create = Func<object, Popup>;
	using Dic = Dictionary<PopupKey, Func<object, Popup>>;

	public enum PopupKey { }

	public static class PopupDB
	{
		public static readonly Dic sDic = new Dic();

		public static Popup Create(PopupKey _key, object _arg)
		{
			Create _creator;
			return sDic.TryGet(_key, out _creator) 
				? _creator(_arg) : null;
		}

		public static void Add(PopupKey _key, Create _creator)
		{
			sDic[_key] = _creator;
		}
	}
}
