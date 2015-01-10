#if UNITY_EDITOR
using System.Diagnostics;

namespace Gem
{
	public class CallerInfo
	{
		public CallerInfo(int _depth)
		{
			var _method = new StackTrace().GetFrame(_depth + 1).GetMethod();

			if ((_method == null)
			    || (_method.ReflectedType == null))
			{
				return;
			}

			method = _method.Name;
			var _class = _method.ReflectedType;
			class_ = _class.Name;
			id = (ClassID) _class.GetHashCode();
		}

		public readonly ClassID id;
		public readonly string class_;
		public readonly string method;
		public string descript { get { return class_ + "." + method; } }
	}
}

#endif