using System.Collections.Generic;

namespace Gem
{
	public class SetBool<T>
	{
		private readonly HashSet<T> mSet = new HashSet<T>();

		public bool Add(T _val)
		{
			var _old = (bool) this;
			mSet.TryAdd(_val);
			return this != _old;
		}

		public bool Remove(T _val)
		{
			var _old = (bool)this;
			mSet.TryRemove(_val);
			return this != _old;
		}

		public static implicit operator bool(SetBool<T> _this)
		{
			return !_this.mSet.Empty();
		}
	}

}