using System;
using UnityEngine;

namespace Gem.In
{
	[Serializable]
	public partial class InputFetcherButton : IInputFetcher
	{
		public string name;

		public void Fetch(ref InputState _org)
		{
			if (string.IsNullOrEmpty(name))
			{
				L.Log(2, L.DO_RETURN_, L.SHOULD_NOT_NULL(name));
				return;
			}

			if (Input.GetButtonDown(name))
				_org.Down();
			else if (Input.GetButtonUp(name))
				_org.Up();
			else if (Debug.isDebugBuild)
			{
				if (Input.GetButton(name) != _org.isOn)
					L.Log(1, L.DO_NOTHING(), L.INVALID_STATE());
			}
		}
	}
}