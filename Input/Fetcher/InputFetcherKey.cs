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
#if UNITY_EDITOR
				if (Input.GetKey(code) != _org.isOn)
					L.W(L.DO.NOTHING, L.M.STATE_INVALID);
#endif
			}
		}
	}
}