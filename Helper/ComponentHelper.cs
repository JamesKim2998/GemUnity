using UnityEngine;

namespace Gem
{
	public static class ComponentHelper
	{
		public static GameObject Instantiate(this GameObject _go)
		{
			return (GameObject) Object.Instantiate(_go);
		}

		public static T Instantiate<T>(this T _comp) where T : Component
		{
			return _comp.gameObject.Instantiate().GetComponent<T>();
		}

		public static T AddIfNotExists<T>(this GameObject _go) where T : Component
		{
			var _comp = _go.GetComponent<T>();
			return _comp ?? _go.AddComponent<T>();
		}
	}
}