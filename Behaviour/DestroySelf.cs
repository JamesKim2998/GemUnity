using UnityEngine;

namespace Gem
{
	public class DestroySelf : MonoBehaviour
	{
		public void Execute()
		{
			Destroy(gameObject);
		}
	}
}