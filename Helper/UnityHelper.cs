using UnityEngine;

namespace Gem
{
	public static class UnityHelper
	{
		private static byte HexToDec(char _c)
		{
			if (_c >= '0' && _c <= '9')
				return (byte) (_c - '0');
			if (_c >= 'a' && _c <= 'f')
				return (byte) (_c - 'a' + 10);
			if (_c >= 'A' && _c <= 'F')
				return (byte) (_c - 'A' + 10);
			L.E(L.M.PARSE_FAIL(_c));
			return 0;
		}

		private static byte TwoHexToDec(char _c1, char _c2)
		{
			return (byte) (HexToDec(_c1)*16 + HexToDec(_c2));
		}

		public static bool TryParse(string _raw, out Color32 _c)
		{
			if (!(_raw.Length == 6 || _raw.Length == 8))
			{
				_c = new Color32();
				L.W(L.M.PARSE_FAIL(_raw));
				return false;
			}

			var o = 0;
			if (_raw.Length == 8)
			{
				o = 2;
				_c.a = TwoHexToDec(_raw[0], _raw[1]);
			}
			else
			{
				_c.a = 255;
			}

			_c.r = TwoHexToDec(_raw[o + 0], _raw[o + 1]);
			_c.g = TwoHexToDec(_raw[o + 2], _raw[o + 3]);
			_c.b = TwoHexToDec(_raw[o + 4], _raw[o + 5]);

			return true;
		}
		
	}

}