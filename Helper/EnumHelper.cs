using System;

public static class EnumHelper
{

	public static bool Has<T>(Enum _enum, T _val)
	{
		var _valInt = Convert.ToUInt64(_val);
		return (Convert.ToUInt64(_enum) & _valInt) == _valInt;
    }

	public static bool No<T>(Enum _enum, T _val)
	{
		return ! Has<T>(_enum, _val);
	}

}
