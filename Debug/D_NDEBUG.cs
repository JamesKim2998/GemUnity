#if !UNITY_EDITOR
using System.Diagnostics;

namespace Gem
{
	public static class D
	{
		[Conditional("UNITY_EDITOR")]
		public static void Assert(bool _val) {}
	}
}

#endif