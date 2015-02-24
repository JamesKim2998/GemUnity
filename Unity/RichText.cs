using UnityEngine;

namespace Gem
{
	public class RichText
	{
		public string txt { get; private set; }

		public RichText(string _txt)
		{
			txt = _txt;
		}

		public RichText AddColor(Color _color)
		{
			txt = "<color=#" + _color.ToHex() + ">" + txt + "</color>";
			return this;
		}

		public RichText AddSize(int _size)
		{
			txt = "<size=" + _size + ">" + txt + "</size>";
			return this;
		}

		public static implicit operator string(RichText _this)
		{
			return _this.txt;
		}
	}
}