using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Gem
{
	public class BitSlot<T, C> : IEnumerable<T> 
		where C : IBiConverter<T, int>, new()
	{
		private readonly BitArray mArr;

		public BitSlot(int _max)
		{
			D.Assert(_max < 16);
			mArr = new BitArray(_max);
		}

		private ushort Convert(T _val)
		{
			var _valInt = new C().Convert(_val);
			D.Assert(_valInt.InRangeOI(0, (ushort)mArr.Count));
			return (ushort) (_valInt - 1);
		}

		private static T Convert(ushort _idx)
		{
			return new C().Convert(_idx + 1);
		}

		public bool Has(T _val)
		{
			return mArr.Get(Convert(_val));
		}

		public void Add(T _val)
		{
			mArr.Set(Convert(_val), true);
		}

		public void Remove(T _val)
		{
			mArr.Set(Convert(_val), false);
		}

		public IEnumerator<T> GetEnumerator()
		{
			ushort i = 0;
			foreach (bool _val in mArr)
				if (_val) yield return Convert(i++);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public override string ToString()
		{
			if (mArr.Empty())
				return "";

			var _builder = new StringBuilder();

			foreach (var _val in this)
			{
				_builder.Append(_val);
				_builder.Append('|');
			}

			if (_builder.Length > 0)
				_builder.Remove(_builder.Length - 1, 1);

			return _builder.ToString();
		}
	}
	
	public struct ReadOnlyBitSlot<T, C> : IEnumerable<T> 
		where C : IBiConverter<T, int>, new()
	{
		private readonly BitSlot<T, C> mData;

		public ReadOnlyBitSlot(BitSlot<T, C> _data)
		{
			mData = _data;
		}

		public bool Has(T _val)
		{
			return (mData != null) && mData.Has(_val);
		}

		public IEnumerator<T> GetEnumerator()
		{
			if (mData == null) yield break;
			yield return (T) mData.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public override string ToString()
		{
			return mData.ToString();
		}

		public static implicit operator ReadOnlyBitSlot<T, C>(BitSlot<T, C> _c)
		{
			return new ReadOnlyBitSlot<T, C>(_c);
		}
	}

}