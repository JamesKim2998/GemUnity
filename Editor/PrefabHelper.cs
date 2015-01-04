using UnityEngine;
using System.Collections;
using UnityEditor;

public static class PrefabHelper {
	static public bool IsPrefab(GameObject _go)
	{
		return PrefabUtility.GetPrefabParent(_go) == null
		       && PrefabUtility.GetPrefabObject(_go) != null;
	}

	static public bool IsPrefab(Component _component)
	{
		return IsPrefab(_component.gameObject);
	}
}
