using UnityEngine.UI;

namespace Gem
{
	public static class UIHelper 
	{
		public static void SetA(this Image _this, float _val)
		{
			var _c = _this.color;
			_c.a = _val;
			_this.color = _c;
		}

		public static void SetR(this Image _this, float _val)
		{
			var _c = _this.color;
			_c.r = _val;
			_this.color = _c;
		}

		public static void SetG(this Image _this, float _val)
		{
			var _c = _this.color;
			_c.g = _val;
			_this.color = _c;
		}

		public static void SetB(this Image _this, float _val)
		{
			var _c = _this.color;
			_c.b = _val;
			_this.color = _c;
		}
	}

}