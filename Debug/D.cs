#if UNITY_EDITOR
using System;

namespace Gem
{
	public static class D
	{
		public static void Assert(bool _val)
		{
			if (_val) return;
			var _caller = new CallerInfo(1);
			L.E(_caller.descript + ": assertion failed.");
		}
	}
}

#endif
