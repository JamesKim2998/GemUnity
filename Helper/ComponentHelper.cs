using UnityEngine;

namespace Gem
{
	public static class ComponentHelper
	{
		public static T Instantiate<T>(this T _comp) where T : Component
		{
			return ((GameObject)Object.Instantiate(_comp.gameObject)).GetComponent<T>();
		}
	}
}