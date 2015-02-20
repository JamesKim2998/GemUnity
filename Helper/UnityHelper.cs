using System.Collections.Generic;
using UnityEngine;

namespace Gem
{
	public static class UnityHelper
	{
		private static byte HexToDec(char c)
		{
			if (c >= '0' && c <= '9')
				return (byte) (c - '0');
			if (c >= 'a' && c <= 'f')
				return (byte) (c - 'a' + 10);
			if (c >= 'A' && c <= 'F')
				return (byte) (c - 'A' + 10);
			L.E(L.M.PARSE_FAIL(c));
			return 0;
		}

		private static byte TwoHexToDec(char _c1, char _c2)
		{
			return (byte) (HexToDec(_c1)*16 + HexToDec(_c2));
		}

		private static char DecToHex(byte d)
		{
			return (d < 10)
				? (char) ('0' + d)
				: (char) ('A' + (d - 10));
		}

		private static Pair<char, char> DecToTwoHex(byte d)
		{
			return new Pair<char, char>(
				DecToHex((byte) (d/16)), 
				DecToHex((byte) (d%16)));
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

		public static string ToHex(this Color32 _this)
		{
			var _hex = new List<char>(8);

			byte[] _rgba = {_this.r, _this.g, _this.b, _this.a};

			foreach (var c in _rgba)
			{
				var _twoHex = DecToTwoHex(c);
				_hex.Add(_twoHex.first);
				_hex.Add(_twoHex.second);
			}

			return new string(_hex.ToArray());
		}
	}

}