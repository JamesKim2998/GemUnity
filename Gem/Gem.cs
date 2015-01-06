using Gem.In;
using UnityEngine;
using System.Collections;

namespace Gem
{
	public class Gem : MonoBehaviour
	{
		void Update()
		{
			input.Update();
		}

		public readonly InputManager input = new InputManager();
	}

}