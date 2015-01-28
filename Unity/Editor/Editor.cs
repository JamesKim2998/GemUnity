using UnityEngine;

namespace Gem
{
	public class Editor<T> : UnityEditor.Editor 
		where T: Component
	{
		private T mTarget;
		public new T target {
			get { return mTarget ?? (mTarget = (T) base.target); }
		}

		protected virtual void OnEnable()
		{}
	}

}