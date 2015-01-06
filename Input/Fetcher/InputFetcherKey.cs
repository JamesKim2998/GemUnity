using UnityEngine;

namespace Gem.In
{
	public partial class InputFetcherKey : IInputFetcher
	{
		public KeyCode code;

		public InputState Fetch(InputState _org)
		{
			if (code == 0)
			{
				D.Log(2, D.DO_RETURN_, D.SHOULD_NOT_NULL());
				return _org;
			}

			if (Input.GetKeyDown(code))
				_org.Down();
			else if (Input.GetKeyUp(code))
				_org.Up();
			else if (Debug.isDebugBuild)
			{
				if (Input.GetKey(code) != _org.isOn)
					D.Log(1, D.DO_NOTHING(), D.INVALID_STATE());
			}

			return _org;
		}
	}
}