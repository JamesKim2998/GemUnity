using System;
using UnityEngine;

namespace Gem.In
{
	public static class InputHelper
	{
		public static Vector2 GetAxis()
		{
			return new Vector2(
				Input.GetAxis("Horizontal"),
				Input.GetAxis("Vertical"));
		}

		public static Direction GetDirection(float _deadzone = DirectionHelper.DEADZONE_DEFAULT)
		{
			return GetAxis().ToDirection(_deadzone);
		}

		public static void DecorateDirection(
			this InputGroup _group, 
			Func<Direction, bool> _down, 
			Action<Direction> _update)
		{
			foreach (var _dir in EnumHelper.GetValues<Direction>())
			{
				var _dirCpy = _dir;
				var _handler = new InputHandler { down = () => _down(_dirCpy) };
				if (_handler.listen = (_update != null))
					_handler.update = () => _update(_dirCpy);
				_group.Add(_dir.ToInputCode(), _handler);
			}

			_group.Reg();
		}
	}

}