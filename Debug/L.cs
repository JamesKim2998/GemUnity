namespace Gem
{
	public static partial class L
	{
		public enum V { N = -1, D = 0, W = 1, E = 2 }

#if UNITY_EDITOR
		public static readonly bool d = true;
#else 
		public const bool d = false;
#endif

	}

}