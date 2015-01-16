using UnityEngine;

namespace Gem
{

	public static class MathHelper 
	{
		public static int RandPosInt()
		{
			return Random.Range(0, int.MaxValue);
		}
	}

}