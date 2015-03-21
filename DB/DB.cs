using System.Collections;
using System.Collections.Generic;
using FullInspector;

namespace Gem
{
	public interface IDB
	{
#if UNITY_EDITOR
		void Build();
#endif
	}

	public class DB<Key, Value> : BaseScriptableObject, IDB, IEnumerable<KeyValuePair<Key, Value>>
		where Value : IDBKey<Key>
	{
		public Dictionary<Key, Value> editorDic;

		public Value this[Key _key]
		{
			get
			{
				Value _ret;
				editorDic.TryGet(_key, out _ret);
				return _ret;
			}
		}

		public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
		{
			return editorDic.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

#if UNITY_EDITOR
		public virtual void Build()
		{
			foreach (var _kv in editorDic)
				_kv.Value.key = _kv.Key;
		}
#endif
	}
}