using System;
using UnityEngine;

namespace Gem.In
{
	[Serializable]
	public partial class InputFetcherKey : IInputFetcher
	{
		public KeyCode code;

		public void Fetch(ref InputState _org)
		{
			if (code == 0)
			{
				L.Log(2, L.DO_RETURN_, L.SHOULD_NOT_NULL());
				return;
			}

			if (Input.GetKeyDown(code))
				_org.Down();
			else if (Input.GetKeyUp(code))
				_org.Up();
			else if (App.d)
			{
				if (Input.GetKey(code) != _org.isOn)
					L.Log(1, L.DO_NOTHING(), L.INVALID_STATE());
			}
		}
	}
}