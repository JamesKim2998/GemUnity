using UnityEngine;

namespace Gem
{
	public static class MetricHelper
	{
		public static Vector2 MultOTO(this Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}
	}

}