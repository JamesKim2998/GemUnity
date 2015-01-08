using System;
using Gem.In;
using UnityEngine;
using System.Collections;

namespace Gem
{
	public class Gem : MonoBehaviour
	{
		void Awake()
		{
			var _appAwake = App.playing;
		}

		void Update()
		{
			input.Update();
		}

		public readonly InputManager input = new InputManager();
	}

}