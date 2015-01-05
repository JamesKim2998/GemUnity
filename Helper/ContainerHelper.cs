using System;
using System.Collections.Generic;

namespace Gem
{

	public enum PositionType
	{
		FRONT,
		BACK,
	}

	public static class ContainerHelper
	{
		public static bool Remove<T>(List<T> _c, Predicate<T> _pred)
		{
			var i = 0;

			foreach (var _data in _c)
			{
				if (_pred(_data))
				{
					_c.RemoveAt(i);
					return true;
				}

				i++;
			}

			return false;
		}

		public static void Add<T>(LinkedList<T> _c, T _val, PositionType _position)
		{
			if (_position == PositionType.FRONT)
				_c.AddFirst(_val);
			else
				_c.AddLast(_val);
		}

		public static bool Remove<T>(LinkedList<T> _c, Predicate<T> _pred)
		{
			for (var _node = _c.First; _node != null; _node = _node.Next)
			{
				if (_pred(_node.Value))
				{
					_c.Remove(_node);
					return true;
				}
			}

			return false;
		}

	}
}