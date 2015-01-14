using UnityEngine;

namespace Gem
{
	public static class MetricHelper
	{
		public static Vector2 DegToVector(float _degree)
		{
			return new Vector2(
				Mathf.Cos(Mathf.Deg2Rad * _degree),
				Mathf.Sin(Mathf.Deg2Rad * _degree));
		}

		public static float ToRad(this Vector2 _vector)
		{
			return Mathf.Atan2(_vector.y, _vector.x);
		}

		public static float ToDeg(this Vector2 _vector)
		{
			return Mathf.Rad2Deg * _vector.ToRad();
		}

		public static Vector2 MultOTO(this Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}
	}

}