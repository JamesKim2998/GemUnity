using UnityEngine;
using System.Collections;

public static class ComponentHelper {
	public static T Instantiate<T>(T _component) where T: Component
	{
		return ((GameObject)Object.Instantiate(_component.gameObject)).GetComponent<T>();
	}
}
