#pragma warning disable 0168

using UnityEngine;

namespace Gem
{
	
	public class GemInit : MonoBehaviour
	{
		private static bool sIsInited;

		void Start ()
		{
			if (sIsInited) return;
			sIsInited = true;

			var _gemAwake = TheGem.g;
		}
	}

}