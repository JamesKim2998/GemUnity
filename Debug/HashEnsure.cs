#if UNITY_EDITOR
using System.Collections.Generic;
#endif

namespace Gem
{
#if UNITY_EDITOR
	using Table = Dictionary<int, string>;
#endif

	public static class HashEnsure
	{
#if UNITY_EDITOR
		private static readonly Table mDic = new Table();
#endif

		public static int Do(string _str)
		{
			var _val = _str.GetHashCode();

#if UNITY_EDITOR
			string _dicStr;
			if (mDic.TryGetValue(_val, out _dicStr))
				D.Assert(_dicStr == _str);
			else
				mDic.Add(_val, _str);
#endif

			return _val;
		}
	}

}