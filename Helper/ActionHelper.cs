using System;

namespace Gem
{
	public static class ActionHelper
	{
		public static void CheckAndCall(this Action _act)
		{
			if (_act != null) _act();
		}

		public static void CheckAndCall<T1>(this Action<T1> _act, ref T1 _p1)
		{
			if (_act != null) _act(_p1);
		}

		public static void CheckAndCall<T1, T2>(this Action<T1, T2> _act, ref T1 _p1, ref T2 _p2)
		{
			if (_act != null) _act(_p1, _p2);
		}

		public static void CheckAndCall<T1, T2, T3>(this Action<T1, T2, T3> _act, ref T1 _p1, ref T2 _p2, ref T3 _p3)
		{
			if (_act != null) _act(_p1, _p2, _p3);
		}

	}
}