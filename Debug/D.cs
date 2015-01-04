using UnityEngine;
using System.Collections;

public static class D {

	public static void Log(int _l, string _msg)
	{
		if (! Debug.isDebugBuild)
			return;

		switch (_l)
		{
			case 0: 
				Debug.Log(_msg);
				break;

			case 1:
				Debug.LogWarning(_msg);
				break;

			default:
				Debug.LogError(_msg);
				break;
		}
	}
}

