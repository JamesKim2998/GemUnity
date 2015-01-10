using UnityEngine;

namespace Gem
{
	public struct Point
	{
		public Point(int _x, int _y)
		{
			x = _x;
			y = _y;
		}

		public int x;
		public int y;

		public static implicit operator Vector2(Point _this)
		{
			return new Vector2(_this.x, _this.y);
		}
	}
}