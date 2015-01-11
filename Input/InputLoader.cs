using UnityEngine;

namespace Gem.In
{
	public class InputLoader : MonoBehaviour
	{
		public string inputFile;

		void Start()
		{
			var _data = JsonHelper.DataWithRaw(new Path_(inputFile));
			TheGem.g.input.map.Load(_data);
		}
	}
}