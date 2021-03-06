﻿#if UNITY_EDITOR
using UnityEditor;

namespace Gem
{
	public static class Asset
	{
		public static T Load<T>(Path _path) where T : class
		{
			var _ret = AssetDatabase.LoadAssetAtPath(_path, typeof (T)) as T;
			if (_ret == null) L.W(L.M.RSC_NOT_EXISTS(_path));
			return _ret;
		}
	}
}

#endif