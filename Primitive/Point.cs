﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using LitJson;
using UnityEngine;

namespace Gem
{
	[DebuggerDisplay("x = {x}, y = {y}")]
	[Serializable]
	public struct Point
	{
		public static readonly Point ZERO = new Point();
		public static readonly Point ONE = new Point(1, 1);
		public static readonly Point NULL = new Point(-51351215, -87453421);

		public Point(int _x, int _y)
		{
			x = _x;
			y = _y;
		}

		public Point(Direction _dir)
			: this(_dir.HMag(), _dir.VMag())
		{}

		public Point(Vector2 _v)
			: this((int) (_v.x + float.Epsilon), (int) (_v.y + float.Epsilon))
		{
#if UNITY_EDITOR
			if ((Math.Abs(_v.x - x) > 2 * float.Epsilon)
				|| (Math.Abs(_v.y - y) > 2 * float.Epsilon))
			{
				L.W(L.M.CONV_NARROW);
			}
#endif
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

		public Point MultOTO(Point p)
		{
			return new Point(x * p.x, y * p.y);
		}

		public IEnumerable<Point> Range()
		{
			for (var _x = 0; _x != x; ++_x)
				for (var _y = 0; _y != y; ++_y)
					yield return new Point(_x, _y);
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

		public static Point operator /(Point _this, int _val)
		{
			if (!((_this.x%_val == 0) && (_this.y%_val == 0)))
				L.W(L.M.CONV_NARROW);
			return new Point(_this.x/_val, _this.y/_val);
		}

		public static explicit operator Direction(Point _this)
		{
			var _ret = default(Direction);

			if (_this.x > 0)
				_ret |= Direction.R;
			else if (_this.x < 0)
				_ret |= Direction.L;

			if (_this.y > 0)
				_ret |= Direction.U;
			else if (_this.y < 0)
				_ret |= Direction.D;

			return _ret;
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

		public static Point Parse(JsonData _json)
		{
			return new Point((int) _json[0], (int) _json[1]);
		}

		public static bool TryParse(JsonData _json, out Point p)
		{
			if (!_json.IsArray || !_json[0].IsInt || !_json[1].IsInt)
			{
				p = ZERO;
				return false;
			}

			p = Parse(_json);
			return true;
		}
	}
}