using Newtonsoft.Json;
using UnityEngine;

namespace Gem.In
{

	public partial class InputFetcherButton : IInputFetcher
	{
		public string name;

		public InputState Fetch(InputState _org)
		{
			if (string.IsNullOrEmpty(name))
			{
				D.Log(2, D.DO_RETURN_, D.SHOULD_NOT_NULL(name));
				return _org;
			}

			if (Input.GetButtonDown(name))
				_org.Down();
			else if (Input.GetButtonUp(name))
				_org.Up();
			else if (Debug.isDebugBuild)
			{
				if (Input.GetButton(name) != _org.isOn)
					D.Log(1, D.DO_NOTHING(), D.INVALID_STATE());
			}

			return _org;
		}
	}
}