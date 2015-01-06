using System;

public static class EnumHelper
{

	public static bool Has<T>(this Enum _enum, T _val)
	{
		var _valInt = Convert.ToUInt64(_val);
		return (Convert.ToUInt64(_enum) & _valInt) == _valInt;
    }

	public static bool No<T>(this Enum _enum, T _val)
	{
		return ! Has(_enum, _val);
	}

	public static bool TryParse<T>(string _str, out T _ret)
	{
		_ret = default(T);
		int _intVal;

		if (Int32.TryParse(_str, out _intVal))
		{
			if (Enum.IsDefined(typeof(T), _intVal))
			{
				_ret = (T)(object)_intVal;
				return true;
			}
		}

		return false;
	}
}
