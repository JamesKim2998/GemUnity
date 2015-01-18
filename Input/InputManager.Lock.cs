using System.Diagnostics;

namespace Gem.In
{

	public partial class InputManager
	{

#if UNITY_EDITOR
		private int mLock;
#endif

		public bool IsDebugLocked()
		{
#if UNITY_EDITOR
			return mLock != 0;
#else
			return false;
#endif
		}

		[Conditional("UNITY_EDITOR")]
		public void DebugLock(bool _lock, int _key)
		{
#if UNITY_EDITOR
			D.Assert(!_lock || (mLock == 0));
			D.Assert(_lock || (mLock == _key));
			mLock = _lock ? _key : 0;
#endif
		}

	}

}