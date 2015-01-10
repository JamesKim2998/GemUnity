#if UNITY_EDITOR
using System;

namespace Gem
{
	public static class D
	{
		public static void Assert(bool _val)
		{
			throw new Exception("assertion failed.");
		}
	}
}

#endif
