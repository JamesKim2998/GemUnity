using UnityEngine;

namespace Gem
{
	public static class RscHelper {

		public static Object Load(string _file)
		{
			var _rsc = Resources.Load(_file);
			if (_rsc == null)
			{
				L.Log(2, L.DO_RETURN_NULL, L.RSC_NOT_EXISTS(_file));
				return null;
			}

			return _rsc;
		}

		public static TextAsset Text(string _file)
		{
			var _rsc = Load(_file);
			if (_rsc == null) return null;

			var _text = Resources.Load(_file) as TextAsset;
			if (_text == null)
			{
				L.Log(2, L.DO_RETURN_NULL, L.CAST_FAIL);
				return null;
			}

			return _text;
		}
	}

}