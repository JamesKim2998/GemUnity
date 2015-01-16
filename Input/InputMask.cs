using System.Collections.Generic;
using Gem.In;

namespace Gem
{

	public struct InputMask
	{
		private const int SIZE = InputCodeHelper.COUNT;

		private readonly SortedList<InputMaskKey, byte>[] mMask;

		public InputMask(int _unused)
		{
			mMask = new SortedList<InputMaskKey, byte>[SIZE];
			for (var i = 0; i != SIZE; ++i)
				mMask[i] = new SortedList<InputMaskKey, byte>();
		}

		public bool Check(InputCode _code)
		{
			return !mMask[(int)_code].Empty();
		}

		public void On(InputMaskKey _key, InputCode[] _mask)
		{
			foreach (var _code in _mask)
				mMask[(int)_code].TryAdd(_key, (byte)0);
		}

		public void Off(InputMaskKey _key)
		{
			foreach (var _mask in mMask)
				_mask.TryRemove(_key);
		}

		public void Off(InputMaskKey _key, InputCode[] _mask)
		{
			foreach (var _code in _mask)
				mMask[(int)_code].TryRemove(_key);
		}

		public bool this[InputCode _code]
		{
			get { return Check(_code); }
		}
	}

}