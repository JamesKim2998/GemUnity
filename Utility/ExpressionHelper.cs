using UnityEngine;

namespace Gem.Expression
{
	public static class ExpressionHelper
	{
		public static void AddCommon(Bind _bind)
		{
			_bind.Add("DISTANCE_LESS", _args =>
			{
				var a = (Vector2) _args[0];
				var b = (Vector2) _args[1];
				var r = (float) _args[2];
				return (a - b).sqrMagnitude < (r*r);
			});
		}
	}
}
