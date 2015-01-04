using UnityEngine;
using System.Collections;

public static class InputHelper {
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
