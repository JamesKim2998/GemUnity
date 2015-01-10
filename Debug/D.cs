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
			throw new Exception(_caller.descript + ": assertion failed.");
		}
	}
}

#endif
