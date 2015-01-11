using System;
using UnityEngine;

namespace Gem
{
	[Flags]
	[Serializable]
	public enum Direction
	{
		L = 1 << 0,
		R = 1 << 1,
		U = 1 << 2,
		D = 1 << 3,
	}

	public static class DirectionHelper
	{
		public const int COUNT = 4;
		public const float DEADZONE_DEFAULT = 0.001f;

		public static bool IsPointing(this Direction _dir)
		{
			return !(_dir.Has(Direction.R) && _dir.Has(Direction.L))
			       && !(_dir.Has(Direction.U) && _dir.Has(Direction.D));
		}

		public static int HMag(this Direction _dir)
		{
			D.Assert(_dir.IsPointing());
			if (_dir.Has(Direction.L))
				return -1;
			if (_dir.Has(Direction.R))
				return 1;
			return 0;
		}

		public static int VMag(this Direction _dir)
		{
			D.Assert(_dir.IsPointing());
			if (_dir.Has(Direction.D))
				return -1;
			if (_dir.Has(Direction.U))
				return 1;
			return 0;
		}

		public static Direction Neg(this Direction _dir)
		{
			return -new Point(_dir);
		}

		public static Direction Rand()
		{
			return (Direction) (1 << UnityEngine.Random.Range(0, COUNT));
		}

		public static Direction ToDirection(this Vector2 _vec, float _deadzone = DEADZONE_DEFAULT)
		{
			var _ret = new Direction();

			if (_vec.x > _deadzone)
				_ret |= Direction.R;
			else if (_vec.x < -_deadzone)
				_ret |= Direction.L;

			if (_vec.y > _deadzone)
				_ret |= Direction.U;
			else if (_vec.y < -_deadzone)
				_ret |= Direction.D;

			return _ret;
		}

		public static Vector2 ToVector2(this Direction _dir)
		{
			D.Assert(IsPointing(_dir));
			return new Vector2(_dir.HMag(), _dir.VMag());
		}

		public static Direction MakeWithAbbr(string _abbr)
		{
			var _ret = default(Direction);

			foreach (var _c in _abbr)
			{
				Direction _dir;
				if (!EnumHelper.TryParse(_c.ToString(), out _dir))
					continue;
				_ret |= _dir;
			}

			return _ret;
		}
	}
}