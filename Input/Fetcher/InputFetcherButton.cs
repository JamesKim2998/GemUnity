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
#if UNITY_EDITOR
				if (Input.GetButton(name) != _org.isOn)
					L.W(L.DO.NOTHING, L.M.STATE_INVALID);
#endif
			}
		}
	}
}