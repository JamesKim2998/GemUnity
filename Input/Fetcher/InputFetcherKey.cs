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
				L.E(L.DO.RETURN_, L.M.SHOULD_NOT_NULL_);
				return;
			}

			if (Input.GetKeyDown(code))
				_org.Down();
			else if (Input.GetKeyUp(code))
				_org.Up();
			else
			{
				var _on = Input.GetKey(code);

				if (_on != _org.isOn)
				{
					L.W("invalid fetch state. fix state.");
					if (_on) _org.Down();
					else _org.Up();
				}
			}
		}
	}
}