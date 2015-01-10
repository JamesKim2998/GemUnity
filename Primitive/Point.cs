using System;
using System.Diagnostics;
using UnityEngine;

namespace Gem
{
	[DebuggerDisplay("x = {x}, y = {y}")]
	public struct Point
	{
		public Point(int _x, int _y)
		{
			x = _x;
			y = _y;
		}

		public Point(Direction _dir)
		{
			D.Assert(_dir.IsPointing());
			x = _dir.HMag();
			y = _dir.VMag();
		}

		public Point(Vector2 _v)
		{
			x = (int) _v.x;
			y = (int) _v.y;

			if ((Math.Abs(_v.x - x) > float.Epsilon)
				|| (Math.Abs(_v.y - y) > float.Epsilon))
			{
				L.W(L.M.CONV_NARROW);
			}
		}

		public int x;
		public int y;

		public bool Inner(Point p)
		{
			return (x < p.x) && (y < p.y);
		}

		public bool Outer(Point p)
		{
			return (x > p.x) && (y > p.y);
		}

		public static bool operator ==(Point a, Point b)
		{
			return (a.x == b.x)
			       && (a.y == b.y);
		}

		public static bool operator !=(Point a, Point b)
		{
			return !(a == b);
		}

		public static Point operator +(Point a, Point b)
		{
			return new Point(a.x + b.x, a.y + b.y);
		}

		public static Point operator -(Point _this)
		{
			return new Point(-_this.x, -_this.y);
		}

		public static Point operator -(Point a, Point b)
		{
			return a + (-b);
		}

		public static implicit operator Vector2(Point _this)
		{
			return new Vector2(_this.x, _this.y);
		}

		public bool Equals(Point _p)
		{
			return x == _p.x && y == _p.y;
		}

		public override bool Equals(object _obj)
		{
			if (ReferenceEquals(null, _obj)) return false;
			return _obj is Point && Equals((Point)_obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (x * 397) ^ y;
			}
		}

		public override string ToString()
		{
			return "(" + x + ", " + y + ")";
		}
	}
}