namespace Gem
{
	public struct TrianglarFloat
	{
		private readonly float mMin;
		private readonly float mMax;
		private float mValue;

		public bool isFalling { get; private set; }

		public TrianglarFloat(float _max, float _min = 0)
			: this()
		{
			D.Assert(_max > _min);
			mMax = _max;
			mValue = mMin = _min;
		}

		public bool Add(float _val)
		{
			D.Assert(_val > 0);

			if (isFalling)
			{
				mValue -= _val;
				if (mValue < mMin)
				{
					isFalling = false;
					mValue = 2 * mMin - mValue;
					return true;
				}
			}
			else
			{
				mValue += _val;
				if (mValue > mMax)
				{
					isFalling = true;
					mValue = 2 * mMax - mValue;
					return true;
				}
			}

			return false;
		}

		public static implicit operator float(TrianglarFloat _val)
		{
			return _val.mValue;
		}
	}
}
