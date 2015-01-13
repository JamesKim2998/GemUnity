#pragma warning disable 0168

using UnityEngine;

namespace Gem
{
	
	public class GemInit : MonoBehaviour {

		void Start ()
		{
			var _gemAwake = TheGem.g;
		}
	}

}