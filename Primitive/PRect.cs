using System;
using UnityEngine;

namespace Gem
{
	public struct PRect
	{
		public Point org;
		public Point dst;

		public PRect(Point p)
		{
			org = p;
			dst = p;
		}

		public int ox
		{
			get { return org.x; }
			set { org.x = value; }
		}

		public int oy
		{
			get { return org.y; }
			set { org.y = value; }
		}

		public int dx
		{
			get { return dst.x; }
			set { dst.x = value; }
		}

		public int dy
		{
			get { return dst.y; }
			set { dst.y = value; }
		}

		public int w
		{
			get { return dx - ox; }
			set { dx = ox + value; }
		}

		public int h
		{
			get { return dy - oy; }
			set { dy = oy + value; }
		}

		public Vector2 c
		{
			get { return ((Vector2) (org + dst))/2; }
			set
			{
				var _size = (Vector2) size;
				org = new Point(value - _size/2);
				dst = new Point(value + _size/2);
			}
		}

		public Point size
		{
			get { return dst - org; }
			set { dst = org + value; }
		}

		public bool IsSorted()
		{
			return (w >= 0) && (h >= 0);
		}

		public void Sort()
		{
			if (ox > dx)
				CSharpHelper.Swap(ref org.x, ref dst.x);

			if (oy > dy)
				CSharpHelper.Swap(ref org.y, ref dst.y);
		}

		public bool Contains(Point p)
		{
			D.Assert(IsSorted());
			return p.x.InRangeII(ox, dx)
				|| p.y.InRangeII(oy, dy);
		}

		public bool Overlaps(PRect r)
		{
			D.Assert(IsSorted() && r.IsSorted());
			return (ox <= r.dx) && (dx >= r.ox)
			       && (oy <= r.dy) && (dy >= r.oy);
		}
	}
}
