using System;
using System.Collections.Generic;
using System.Linq;

namespace Gem
{

	public enum PositionType
	{
		FRONT,
		BACK,
	}

	public static class CollectionHelper
	{
		public static bool Remove<T>(this List<T> _c, Predicate<T> _pred)
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

		public static void Add<T>(this LinkedList<T> _c, T _val, PositionType _position)
		{
			if (_position == PositionType.FRONT)
				_c.AddFirst(_val);
			else
				_c.AddLast(_val);
		}

		public static bool Remove<T>(this LinkedList<T> _c, Predicate<T> _pred)
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

		public static bool TryAdd<K, V>(this IDictionary<K, V> _c, K _key, V _val)
		{
			try
			{
				_c.Add(_key, _val);
				return true;
			}
			catch (Exception)
			{
				L.Log(0, L.DO_RETURN(false), L.KEY_EXISTS(_key));
				return false;
			}
		}

		public static bool GetAndRemove<K, V>(this IDictionary<K, V> _c, K _key, out V _val)
		{
			if (_c.TryGetValue(_key, out _val))
			{
				_c.Remove(_key);
				return true;
			}
			else
			{
				L.Log(2, L.DO_RETURN(false), L.KEY_NOT_EXISTS(_key));
				return false;
			}
		}
	}
}