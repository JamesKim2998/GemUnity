using System;
using UnityEngine;

namespace Gem
{
	[Flags]
	public enum Direction
	{
		L = 1 << 0,
		R = 1 << 1,
		U = 1 << 2,
		D = 1 << 3,
	}

	public static class DirectionHelper
	{
		public const float DEADZONE_DEFAULT = 0.001f;

		public static Direction ToDirection(Vector2 _vec, float _deadzone = DEADZONE_DEFAULT)
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

		public static Vector2 ToVector2(Direction _dir)
		{
			var _ret = Vector2.zero;

			if (_dir.Has(Direction.R))
				_ret.x = 1;
			else if (_dir.Has(Direction.L))
				_ret.x = -1;

			if (_dir.Has(Direction.U))
				_ret.y = 1;
			else if (_dir.Has(Direction.D))
				_ret.y = -1;

			return _ret;
		}
	}
}