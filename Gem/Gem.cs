#pragma warning disable 0168

using Gem.In;
using UnityEngine;

namespace Gem
{
	public class Gem : MonoBehaviour
	{
		void Awake()
		{
			var _appAwake = App.playing;
			var _brAwake = Broadcaster.g;
		}

		void Update()
		{
			input.Update();
		}

		public readonly InputManager input = new InputManager();
	}

}