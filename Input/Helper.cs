using UnityEngine;
using System.Collections;

namespace In
{
	public static class Helper
	{
		public static Vector2 GetAxis()
		{
			return new Vector2(
				Input.GetAxis("Horizontal"),
				Input.GetAxis("Vertical"));
		}

		public static Direction GetDirection(float _deadzone = DirectionHelper.DEADZONE_DEFAULT)
		{
			return DirectionHelper.ToDirection(GetAxis(), _deadzone);
		}
	}

}