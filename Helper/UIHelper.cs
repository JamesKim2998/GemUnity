﻿using UnityEngine.UI;

namespace Gem
{
	public static class UIHelper 
	{
		public static void SetA(this Graphic _this, float _val)
		{
			var _c = _this.color;
			_c.a = _val;
			_this.color = _c;
		}

		public static void SetR(this Graphic _this, float _val)
		{
			var _c = _this.color;
			_c.r = _val;
			_this.color = _c;
		}

		public static void SetG(this Graphic _this, float _val)
		{
			var _c = _this.color;
			_c.g = _val;
			_this.color = _c;
		}

		public static void SetB(this Graphic _this, float _val)
		{
			var _c = _this.color;
			_c.b = _val;
			_this.color = _c;
		}

		public static string RichAddColor(string _txt, string _color)
		{
			return string.Format("<color=#{0}>{1}</color>", _color, _txt);
		}
	}

}