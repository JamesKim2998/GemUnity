using System;
using System.Collections.Generic;

namespace Gem
{
	public static class EnumHelper
	{
		public static bool Has<T>(this Enum _enum, T _val)
		{
			var _valInt = Convert.ToUInt64(_val);
			return (Convert.ToUInt64(_enum) & _valInt) == _valInt;
		}

		public static bool No<T>(this Enum _enum, T _val)
		{
			return !Has(_enum, _val);
		}

		public static IEnumerable<T> GetValues<T>()
		{
			return (IEnumerable<T>) Enum.GetValues(typeof(T));
		}
		
		// this T _enum으로 하면 모든 타입에 메소드가 정의되어서 this를 뺍니다.
		public static bool IsSingular<T>(T _enum)
		{
			return Enum.IsDefined(typeof(T), _enum);
		}

		public static bool TryParse<T>(string _str, out T _ret)
		{
			if (Enum.IsDefined(typeof(T), _str))
			{
				_ret = (T)Enum.Parse(typeof(T), _str, true);
				return true;
			}

			_ret = default(T);
			L.D(L.DO.RETURN(false), L.M.ENUM_UNDEFINED(_str));
			return false;
		}

		public static bool TryParseAsInt<T>(string _str, out T _ret)
		{
			int _intVal;

			if (Int32.TryParse(_str, out _intVal))
			{
				if (Enum.IsDefined(typeof(T), _intVal))
				{
					_ret = (T)(object)_intVal;
					return true;
				}
			}

			_ret = default(T);
			L.D(L.DO.RETURN(false), L.M.ENUM_UNDEFINED(_str));
			return false;
		}
	}

}