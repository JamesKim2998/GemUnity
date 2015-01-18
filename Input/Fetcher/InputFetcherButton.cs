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
				L.E(L.DO.RETURN_, L.M.SHOULD_NOT_NULL("name"));
				return;
			}

			if (Input.GetButtonDown(name))
				_org.Down();
			else if (Input.GetButtonUp(name))
				_org.Up();
			else
			{
				var _on = Input.GetButton(name);

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