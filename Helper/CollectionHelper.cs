using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;

namespace Gem
{

	public enum PositionType
	{
		FRONT,
		BACK,
	}

	public static class CollectionHelper
	{
		public static bool Empty<T>(this T _c) where T : ICollection
		{
			return _c.Count == 0;
		}

		public static IEnumerable<T> GetReverseEnum<T>(this T[] _c)
		{
			for (var i = _c.Length - 1; i >= 0; i--)
				yield return _c[i];
		}

		public static bool RemoveBack<T>(this List<T> _c)
		{
			if (_c.Count == 0)
				return false;
			_c.RemoveAt(_c.Count - 1);
			return true;
		}

		public static bool RemoveIf<T>(this List<T> _c, Predicate<T> _pred)
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

		public static T FindAndRemoveIf<T>(this List<T> _c, Predicate<T> _pred) 
		{
			var i = 0;

			foreach (var _data in _c)
			{
				if (_pred(_data))
				{
					_c.RemoveAt(i);
					return _data;
				}

				i++;
			}

			return default(T);
		}

		public static void Resize<T>(this List<T> _c, int _size, T _init = default(T))
		{
			var _curSize = _c.Count;

			if (_size < _curSize)
			{
				_c.RemoveRange(_size, _curSize - _size);
			}
			else if (_size > _curSize)
			{
				if (_size > _c.Capacity)
					_c.Capacity = _size;
				_c.AddRange(Enumerable.Repeat(_init, _size - _curSize));
			}
		}

		public static IEnumerable<T> GetReverseEnum<T>(this List<T> _c)
		{
			for (var i = _c.Count - 1; i >= 0; i--)
				yield return _c[i];
		}

		public static int BinarySearch<T>(this IList<T> _c, Func<T, int> _cmp)
		{
			var _min = 0;
			var _max = _c.Count - 1;

			while (_min <= _max)
			{
				var _mid = (_min + _max) / 2;
				var _cmpVal = _cmp(_c[_mid]);

				if (_cmpVal == 0)
					return _mid;

				if (_cmpVal < 0)
					_min = _mid + 1;
				else
					_max = _mid - 1;
			}

			return ~_min;
		}

		public static T Rand<T>(this IList<T> _c)
		{
			D.Assert(_c.Count != 0);
			return _c[UnityEngine.Random.Range(0, _c.Count)];
		}

		public static void Shuffle<T>(this IList<T> c)
		{
			var _rng = new Random();
			var n = c.Count;
			while (n > 1)
			{
				n--;
				var k = _rng.Next(n + 1);
				var value = c[k];
				c[k] = c[n];
				c[n] = value;
			}
		}

		public static void Add<T>(this LinkedList<T> _c, T _val, PositionType _position)
		{
			if (_position == PositionType.FRONT)
				_c.AddFirst(_val);
			else
				_c.AddLast(_val);
		}

		public static bool RemoveIf<T>(this LinkedList<T> _c, Predicate<T> _pred)
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

		public static bool TryGet<K, V>(this IDictionary<K, V> _c, K _key, out V _val)
		{
			if (!_c.TryGetValue(_key, out _val))
			{
				L.W(L.DO.RETURN(false), L.M.KEY_NOT_EXISTS(_key));
				return false;
			}
			return true;
		}

		public static bool TryGetAndParse<K, T>(this IDictionary<K, string> c, K _key, out T _val)
		{
			string _valStr;

			if (!c.TryGet(_key, out _valStr))
			{
				_val = default(T);
				return false;
			}

			return EnumHelper.TryParse(_valStr, out _val);
		}

		public static V GetOrDefault<K, V>(this IDictionary<K, V> _c, K _key, V _default = default(V))
		{
			V _val;
			return _c.TryGetValue(_key, out _val) ? _val : _default;
		}

		public static V GetOrPut<K, V>(this IDictionary<K, V> c, K _key) where V : class, new()
		{
			var _val = c.GetOrDefault(_key);
			if (_val != null) 
				return _val;

			_val = new V();
			c[_key] = _val;
			return _val;
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
				L.W(L.DO.RETURN(false), L.M.KEY_EXISTS(_key));
				return false;
			}
		}

		public static bool TryRemove<K, V>(this IDictionary<K, V> _c, K _key)
		{
			var _ret = _c.Remove(_key);
			if (!_ret) L.D(L.DO.RETURN(false), L.M.KEY_NOT_EXISTS(_key));
			return _ret;
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
				L.E(L.DO.RETURN(false), L.M.KEY_NOT_EXISTS(_key));
				return false;
			}
		}

		public static void RemoveIf<K, V>(this IDictionary<K, V> _c, Predicate<K> _pred)
		{
			var _remove = new List<K>();

			foreach (var _kv in _c)
			{
				if (_pred(_kv.Key))
					_remove.Add(_kv.Key);
			}

			foreach (var _key in _remove)
			{
				_c.Remove(_key);
			}
		}

		public static bool Empty<T>(this HashSet<T> _c)
		{
			return _c.Count == 0;
		}

		public static bool TryAdd<T>(this HashSet<T> _c, T _val)
		{
			if (! _c.Add(_val))
			{
				L.E(L.DO.RETURN(false), L.M.KEY_EXISTS(_val));
				return false;
			}

			return true;
		}

		public static bool TryRemove<T>(this HashSet<T> _c, T _val)
		{
			if (! _c.Remove(_val))
			{
				L.E(L.DO.RETURN(false), L.M.KEY_NOT_EXISTS(_val));
				return false;
			}

			return true;
		}

		public static bool RemoveIf<T>(this HeapPriorityQueue<T> _c, Predicate<T> _pred) where T: PriorityQueueNode
		{
			foreach (var _node in _c)
			{
				if (_pred(_node))
				{
					_c.Remove(_node);
					return true;
				}
			}

			return false;
		}
	}
}