using System;

namespace Gem
{
	public struct SewValue<T, A> where T: IComparable<T> where A: IArithmetic<T>, new()
	{
		public SewValue(T _limit, T _default = default(T))
		{
			mLimit = _limit;
			mValue = mDefault = _default;
		}

		public bool isFall { get { return mDefault.CompareTo(mLimit) < 0; } }

		public bool isDefault { get { return mValue.CompareTo(mDefault) == 0; } }

		public bool Add(T _val)
		{
			mValue = new A { val = this }.Add(new A { val = _val }).val;

			var _cmp = mValue.CompareTo(mLimit);
			var _limit = false;

			if ((isFall && (_cmp > 0)) || (!isFall && (_cmp < 0)))
			{
				mValue = mDefault;
				_limit = true;
			}

			return _limit;
		}

		public static implicit operator T(SewValue<T, A> _val)
		{
			return _val.mValue;
		}

		private readonly T mDefault;
		private readonly T mLimit;
		private T mValue;
	}
}
