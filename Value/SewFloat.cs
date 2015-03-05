namespace Gem
{
	public struct SewFloat
	{
		public SewFloat(float _limit, float _default = default(float))
		{
			mLimit = _limit;
			mValue = mDefault = _default;
		}

		public bool isFall { get { return mDefault.CompareTo(mLimit) < 0; } }

		public bool isDefault { get { return mValue.CompareTo(mDefault) == 0; } }

		public bool Add(float _val)
		{
			mValue += _val;

			var _cmp = mValue.CompareTo(mLimit);
			var _limit = false;

			if ((isFall && (_cmp > 0)) || (!isFall && (_cmp < 0)))
			{
				mValue = mDefault;
				_limit = true;
			}

			return _limit;
		}

		public static implicit operator float(SewFloat _val)
		{
			return _val.mValue;
		}

		private readonly float mDefault;
		private readonly float mLimit;
		private float mValue;
	}
}
