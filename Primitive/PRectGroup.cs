using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Gem
{
	public class PRectGroup
	{
		[DebuggerDisplay("idx={idx}, val={val}")]
		private struct IndexedValue
		{
			public int idx;
			public int val;
		}

		private class Peeker
		{
			public enum Result
			{
				NONE = -1,
			}

			private readonly List<IndexedValue> mList = new List<IndexedValue>();

			private int mCount { get { return mList.Count; } }

			public void Clear()
			{
				mList.Clear();
			}

			public void Sort()
			{
				mList.Sort((a, b) => (a.val - b.val));
			}

			public void AddRange(IEnumerable<IndexedValue> _range)
			{
				mList.AddRange(_range);
			}

			public bool Less(int _val, out Result _idx)
			{
				if (mList.Empty())
				{
					_idx = Result.NONE;
					return false;
				}

				var _found = mList.BinarySearch(_idxVal => _idxVal.val - _val);
				if (_found < 0)
				{
					var _inv = ~_found;
					if (_inv == 0)
					{
						_idx = Result.NONE;
						return false;
					}

					--_inv;
					_idx = (Result) _inv;
					return true;
				}
				else
				{
					if (_found == 0)
					{
						_idx = Result.NONE;
						return false;
					}

					_idx = (Result) (_found - 1);
					return true;
				}
			}

			public bool Greater(int _val, out Result _idx)
			{
				var _found = mList.BinarySearch(_idxVal => _idxVal.val - _val);
				if (_found < 0)
				{
					var _inv = ~_found;
					if (_inv == mCount)
					{
						_idx = Result.NONE;
						return false;
					}

					_idx = (Result)_inv;
					return true;
				}
				else
				{
					if (_found == mCount - 1)
					{
						_idx = Result.NONE;
						return false;
					}

					_idx = (Result)(_found + 1);
					return true;
				}
			}

			public IEnumerable<int> BeginToResult(Result _idx)
			{
				return mList.GetRange(0, (int)_idx + 1).Select<IndexedValue, int>(ToIdx);
			}

			public IEnumerable<int> ResultToEnd(Result _idx)
			{
				return mList.GetRange((int)_idx, mList.Count - (int)_idx).Select<IndexedValue, int>(ToIdx);
			}
		}

		private bool mIsSorted;

		private readonly List<PRect> mRects = new List<PRect>();

		private readonly Peeker mL = new Peeker();
		private readonly Peeker mR = new Peeker();
		private readonly Peeker mT = new Peeker();
		private readonly Peeker mB = new Peeker();

		public int count { get { return mRects.Count; } }

		public void Add(PRect _rect)
		{
			if (mIsSorted)
				mIsSorted = false;

			mRects.Add(_rect);
		}

		private static int ToIdx(IndexedValue _idxVal)
		{
			return _idxVal.idx;
		}

		public List<int> Contains(Point p)
		{
			return Overlaps(new PRect { org = p, size = Point.ONE });
		}

		public List<int> Overlaps(PRect _rect)
		{
			Sort();

			Peeker.Result l, r, b, t;
			if (!mL.Less(_rect.dx, out l)) return null;
			if (!mR.Greater(_rect.ox, out r)) return null;
			if (!mB.Less(_rect.dy, out b)) return null;
			if (!mT.Greater(_rect.oy, out t)) return null;

			var _cnt = count;
			var _idxs = new List<int>(((int)l + 1) + (_cnt - (int)r) + ((int)b + 1) + (_cnt - (int)t));

			_idxs.AddRange(mL.BeginToResult(l));
			_idxs.AddRange(mR.ResultToEnd(r));
			_idxs.AddRange(mB.BeginToResult(b));
			_idxs.AddRange(mT.ResultToEnd(t));
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

		private static void Sort(IEnumerable<PRect> _rects, Func<PRect, int> _toVal, Peeker _out)
		{
			var i = 0;
			_out.AddRange(_rects.Select(_rect => new IndexedValue {val = _toVal(_rect), idx = i++}));
			_out.Sort();
		}
	}
}