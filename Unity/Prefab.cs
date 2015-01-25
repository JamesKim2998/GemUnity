using UnityEngine;

namespace Gem
{
	public struct Prefab
	{
		public Prefab(GameObject _go)
		{
			mGO = _go;
		}

		public GameObject Instantiate()
		{
			return mGO.Instantiate();
		}

		private readonly GameObject mGO;
	}

	public struct Prefab<T> where T: Component
	{
		public Prefab(T _go)
		{
			mGO = _go;
		}

		public T Instantiate()
		{
			return mGO.Instantiate();
		}

		private readonly T mGO;
	}

}