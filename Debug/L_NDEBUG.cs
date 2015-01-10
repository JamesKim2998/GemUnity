#if !UNITY_EDITOR
using System.Diagnostics;

namespace Gem
{
	public static partial class L
	{
		[Conditional("GEM_DC")]
		public static void SetLevel(V _v) {}

		[Conditional("GEM_DC")]
		public static void D(string _msg) {}
		[Conditional("GEM_DC")]
		public static void W(string _msg) {}
		[Conditional("GEM_DC")]
		public static void E(string _msg) {}

		[Conditional("GEM_DC")]
		public static void D(string _do, string _msg) {}
		[Conditional("GEM_DC")]
		public static void W(string _do, string _msg) {}
		[Conditional("GEM_DC")]
		public static void E(string _do, string _msg) {}
	}
}
#endif