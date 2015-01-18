using System.Collections.Generic;

namespace Gem
{
	public class SetBool<T>
	{
		private readonly HashSet<T> mSet = new HashSet<T>();

		public bool Add(T _val) { return mSet.TryAdd(_val); }

		public bool Remove(T _val) { return mSet.TryRemove(_val); }

		public static implicit operator bool(SetBool<T> _this)
		{
			return !_this.mSet.Empty();
		}
	}

}