using System;

namespace Gem
{
	public class ClassWrap<T>
	{
		public T val;

		public static implicit operator T(ClassWrap<T> _this)
		{
			return _this.val;
		}
	}

	public class ActionWrap : ClassWrap<Action> { }
	public class ActionWrap<T1> : ClassWrap<Action<T1>> { }
}