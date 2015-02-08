using UnityEngine;

namespace Gem.Sampler
{
	public struct UniformRect
	{
		public Vector2 max;
		public Vector2 min;

		public UniformRect(Vector2 _max)
		{
			min = Vector2.zero;
			max = _max;
		}

		public UniformRect(Vector2 _min, Vector2 _max)
		{
			min = _min;
			max = _max;
		}

		public Vector2 Sample()
		{
			return new Vector2(
				Random.Range(min.x, max.x),
				Random.Range(min.y, max.y));
		}
	}

}