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

			_c.r = TwoHexToDec(_raw[0], _raw[1]);
			_c.g = TwoHexToDec(_raw[2], _raw[3]);
			_c.b = TwoHexToDec(_raw[4], _raw[5]);

			if (_raw.Length == 8)
				_c.a = TwoHexToDec(_raw[6], _raw[7]);
			else
				_c.a = 255;

			return true;
		}

		public static Color32 ColorOrDefault(string _raw, Color32 _default = default(Color32))
		{
			Color32 c;
			return TryParse(_raw, out c) ? c : _default;
		}

		public static Color32 ToColor32(this Color _this)
		{
			return new Color32(
				(byte)(_this.r * 255),
				(byte)(_this.g * 255),
				(byte)(_this.b * 255),
				(byte)(_this.a * 255));
		}

		public static string ToHex(this Color _this)
		{
			return _this.ToColor32().ToHex();
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

		public static RectTransform RectTransform(this GameObject _this)
		{
			return (RectTransform)_this.transform;
		}

		public static bool Contains(this LayerMask _this, GameObject _target)
		{
			return (_this.value & (1 << _target.layer)) != 0;
		}

		public static bool Contains(this LayerMask _this, Collider2D _target)
		{
			return _this.Contains(_target.gameObject);
		}

		public static bool Play(this Animation _this, AnimationClip _clip)
		{
			_this.clip = _clip;
			return _this.Play();
		}
	}

}