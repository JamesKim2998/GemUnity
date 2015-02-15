#pragma warning disable 0168

using UnityEngine;

namespace Gem
{
	
	public class GemInit : MonoBehaviour
	{
		private static bool sIsInited;

		void Start ()
		{
			if (sIsInited)
			{
				Destroy(gameObject);
				return;
			}

			sIsInited = true;

			var _gemAwake = TheGem.g;

			Destroy(gameObject, 0.1f);
		}
	}

}