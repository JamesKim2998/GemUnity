using System.Linq;
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace Gem
{
	public class PRectGroupTest : MonoBehaviour
	{
		public int x;
		public int y;

		void Start()
		{
			Go();
		}

		public void Go()
		{
			var _rects = new List<PRect>
			{
				new PRect { org = new Point(0, 2), size = new Point(3, 3) }
			};

			var _group = new PRectGroup();
			foreach (var _rect in _rects)
				_group.Add(_rect);

			var _test = new PRect(new Point(x, y));
			var _rA = _group.Overlaps(_test);

			var i = 0;
			var _rB = new List<int>();
			foreach (var _rect in _rects)
			{
				if (_test.Overlaps(_rect))
					_rB.Add(i);
				++i;
			}

			D.Assert(_rA.SequenceEqual(_rB));
		}
	}

}

#endif