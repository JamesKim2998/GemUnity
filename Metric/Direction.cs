using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Flags]
public enum Direction
{
	LEFT = 1 << 0,
	RIGHT = 1 << 1,
	UP = 1 << 2,
	DOWN = 1 << 3,
}

public static class DirectionHelper
{
	public const float DEADZONE_DEFAULT = 0.001f;

	public static Direction ToDirection(Vector2 _vec, float _deadzone = DEADZONE_DEFAULT)
	{
		var _ret = new Direction();

		if (_vec.x > _deadzone)
			_ret |= Direction.RIGHT;
		else if (_vec.x < -_deadzone)
			_ret |= Direction.LEFT;

		if (_vec.y > _deadzone)
			_ret |= Direction.UP;
		else if (_vec.y < -_deadzone)
			_ret |= Direction.DOWN;

		return _ret;
	}

	public static Vector2 ToVector2(Direction _dir)
	{
		var _ret = Vector2.zero;

		if (EnumHelper.Has(_dir, Direction.RIGHT))
			_ret.x = 1;
		else if (EnumHelper.Has(_dir, Direction.LEFT))
			_ret.x = -1;

		if (EnumHelper.Has(_dir, Direction.UP))
			_ret.y = 1;
		else if (EnumHelper.Has(_dir, Direction.DOWN))
			_ret.y = -1;

		return _ret;
	}
}