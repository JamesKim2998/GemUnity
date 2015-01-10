#pragma warning disable 0162
#pragma warning disable 0429

namespace Gem
{
	public static partial class L
	{
		public static class DO
		{
			public static string NOTHING
			{
				get
				{
					return d ? "do nothing." : null;
				}
			}

			public static string CONTINUE
			{
				get
				{
					return d ? "continue." : null;
				}
			}

			public static string BREAK
			{
				get
				{
					return d ? "break." : null;
				}
			}

			public static string RETURN_
			{
				get
				{
					return d ? "return." : null;
				}
			}

			public static string RETURN_NULL
			{
				get
				{
					return d ? "return null." : null;
				}
			}

			public static string RETURN<T>(T _val)
			{
				if (!d) return null;
				return "return " + _val + ".";
			}
		}
	}

}