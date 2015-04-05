using UnityEngine;

namespace Gem
{
	public struct Prefab
	{
		public Prefab(GameObject _go)
		{
			go = _go;
		}

		public GameObject Instantiate()
		{
			return go.Instantiate();
		}

		public readonly GameObject go;
	}

	public struct Prefab<T> where T : Component
	{
		public Prefab(T _go)
		{
			go = _go;
		}

		public T Instantiate()
		{
			return go.Instantiate();
		}

		public readonly T go;
	}

}