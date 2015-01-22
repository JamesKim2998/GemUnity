using System;

namespace Gem
{
	public static class CSharpHelper
	{
		public static void Swap<T>(ref T a, ref T b)
		{
			var _tmp = a;
			b = a;
			a = _tmp;
		}

		// open, open
		public static bool InRangeOO<T>(this T _this, T _from, T _to) where T : IComparable<T>
		{
			D.Assert(_from.CompareTo(_to) <= 0);
			return (_this.CompareTo(_from) > 0) && (_this.CompareTo(_to) < 0);
		}

		// open, inclusive
		public static bool InRangeOI<T>(this T _this, T _from, T _to) where T : IComparable<T>
		{
			D.Assert(_from.CompareTo(_to) <= 0);
			return (_this.CompareTo(_from) > 0) && (_this.CompareTo(_to) <= 0);
		}

		// inclusive, open
		public static bool InRangeIO<T>(this T _this, T _from, T _to) where T : IComparable<T>
		{
			D.Assert(_from.CompareTo(_to) <= 0);
			return (_this.CompareTo(_from) >= 0) && (_this.CompareTo(_to) < 0);
		}

		// inclusive, inclusive
		public static bool InRangeII<T>(this T _this, T _from, T _to) where T : IComparable<T>
		{
			D.Assert(_from.CompareTo(_to) <= 0);
			return (_this.CompareTo(_from) >= 0) && (_this.CompareTo(_to) <= 0);
		}
	}
}