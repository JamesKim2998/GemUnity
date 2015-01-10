namespace Gem
{
	public class Grid<T>
	{
		public Grid(int _w, int _h)
		{
			mGrid = new T[_w, _h];
		}

		public int w { get { return mGrid.Length; } }
		public int h { get { return mGrid.GetLength(1); } }
		public Point size { get { return new Point(w, h); } }

		public T Get(Point _p)
		{
			return mGrid[_p.x, _p.y];
		}

		private readonly T[,] mGrid;
	}

}