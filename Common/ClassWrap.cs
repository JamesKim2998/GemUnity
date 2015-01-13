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
	public class ActionWrap<T1, T2> : ClassWrap<Action<T1, T2>> { }
	public class ActionWrap<T1, T2, T3> : ClassWrap<Action<T1, T2, T3>> { }
}