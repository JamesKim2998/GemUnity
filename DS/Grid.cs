using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gem
{
	[DebuggerDisplay("size = {size}")]
	public class Grid<T> : IEnumerable<T>
	{
		private readonly T[,] mGrid;

		public Grid(Point _size)
		{
			mGrid = new T[_size.x, _size.y];
		}

		public int w { get { return mGrid.Length / h; } }
		public int h { get { return mGrid.GetLength(1); } }
		public Point size { get { return new Point(w, h); } }

		public bool TryGet(Point _p, out T _out)
		{
			if (! _p.Inner(size))
			{
				_out = default(T);
				return false;
			}

			_out = Get(_p);
			return true;
		}

		public T Get(Point _p)
		{
			D.Assert(_p.Inner(size));
			return mGrid[_p.x, _p.y];
		}

		public bool TrySet(Point _p, T _data)
		{
			if (! _p.Inner(size))
				return false;

			Set(_p, _data);
			return true;
		}

		public void Set(Point _p, T _data)
		{
			D.Assert(_p.Inner(size));
			mGrid[_p.x, _p.y] = _data;
		}

		public T this[Point _p]
		{
			get { return Get(_p); }
			set { Set(_p, value); }
		}

		public IEnumerator<T> GetEnumerator()
		{
			foreach (var _data in mGrid)
				yield return _data;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			foreach (var _data in mGrid)
				yield return _data;
		}
	}

}