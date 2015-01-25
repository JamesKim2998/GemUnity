using UnityEngine;

namespace Gem.In
{
	public class InputLoader : MonoBehaviour
	{
		private static readonly Path_ FILE_PATH = new Path_("Resources/Config");
		public string inputFile;

		void Start()
		{
			var _data = JsonHelper.DataWithRaw(new FullPath(FILE_PATH / inputFile));
			InputManager.g.map.Load(_data);
		}
	}
}