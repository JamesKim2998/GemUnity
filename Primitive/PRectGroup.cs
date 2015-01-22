using System;
using System.Collections.Generic;
using System.Linq;

namespace Gem
{
	public class PRectGroup
	{
		private bool mIsSorted;

		private readonly List<PRect> mRects = new List<PRect>();

		private readonly List<IndexedValue> mL = new List<IndexedValue>();
		private readonly List<IndexedValue> mR = new List<IndexedValue>();
		private readonly List<IndexedValue> mT = new List<IndexedValue>();
		private readonly List<IndexedValue> mB = new List<IndexedValue>();

		public int count { get { return mRects.Count; } }

		public void Add(PRect _rect)
		{
			if (mIsSorted)
				mIsSorted = false;

			mRects.Add(_rect);
		}

		private static int OrderByIdx(IndexedValue _idxVal)
		{
			return _idxVal.idx;
		}

		public List<int> Contains(Point p)
		{
			return Overlaps(new PRect(p));
		}

		public List<int> Overlaps(PRect _rect)
		{
			Sort();

			var l = mL.BinarySearch(_idxVal => _idxVal.val - _rect.dx);
			var r = mR.BinarySearch(_idxVal => _idxVal.val - _rect.ox);
			var b = mB.BinarySearch(_idxVal => _idxVal.val - _rect.dy);
			var t = mT.BinarySearch(_idxVal => _idxVal.val - _rect.oy);

			var _cnt = count;

			if ((l == ~0) || (b == ~0)
			    || (r == ~_cnt) || (t == ~_cnt))
			{
				return new List<int>();
			}

			if (l < 0) l = ~l;
			if (r < 0) r = ~r;
			if (b < 0) b = ~b;
			if (t < 0) t = ~t;

			if (l == _cnt) --l;
			if (b == _cnt) --b;

			var _idxs = new List<int>((l + 1) + (_cnt - r) + (b + 1) + (_cnt - t));

			_idxs.AddRange(mL.GetRange(0, l + 1).Select<IndexedValue, int>(OrderByIdx));
			_idxs.AddRange(mR.GetRange(r, _cnt - r).Select<IndexedValue, int>(OrderByIdx));
			_idxs.AddRange(mB.GetRange(0, b + 1).Select<IndexedValue, int>(OrderByIdx));
			_idxs.AddRange(mT.GetRange(t, _cnt - t).Select<IndexedValue, int>(OrderByIdx));
			_idxs.Sort();

			var _lastIdx = -1;
			var _seq = 0;
			var _ret = new List<int>(_idxs.Count / 8);

			foreach (var _idx in _idxs)
			{
				if (_lastIdx != _idx)
				{
					_lastIdx = _idx;
					_seq = 1;
					continue;
				}

				if (++_seq == 4)
					_ret.Add(_idx);
			}

			return _ret;
		}

		struct IndexedValue
		{
			public int idx;
			public int val;
		}

		private void Sort()
		{
			if (mIsSorted)
				return;

			mIsSorted = true;

			mL.Clear();
			mR.Clear();
			mT.Clear();
			mB.Clear();

			Sort(mRects, _rect => _rect.ox, mL);
			Sort(mRects, _rect => _rect.dx, mR);
			Sort(mRects, _rect => _rect.oy, mB);
			Sort(mRects, _rect => _rect.dy, mT);
		}

		private static void Sort(IEnumerable<PRect> _rects, Func<PRect, int> _toVal, List<IndexedValue> _out)
		{
			var i = 0;
			_out.AddRange(_rects.Select(_rect => new IndexedValue {val = _toVal(_rect), idx = i++}));
			_out.Sort((a, b) => (a.val - b.val));
		}
	}
}