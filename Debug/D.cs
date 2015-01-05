using System;
using UnityEngine;

public static class D
{
	public static string INVALID_CALL()
	{
		if (!Debug.isDebugBuild)
			return null;
		return "invalid function call.";
	}

	public static string SHOULD_NOT_NULL<T>(T _val) where T : class
	{
		if (!Debug.isDebugBuild)
			return null;
		if (_val != null)
			throw new Exception("should not null always should be used with value null.");
		return typeof(T).Name + " is null.";
	}

	public static string SHOULD_NULL<T>(T _val) where T: class
	{
		if (!Debug.isDebugBuild)
			return null;
		if (_val == null)
			throw new Exception("should null always should be used with value.");
		return typeof(T).Name + " has value " + _val + ".";
	}

	public static string KEY_EXISTS<T>(T _key)
	{
		if (!Debug.isDebugBuild)
			return null;
		return "key " + _key + " already exists.";
	}

	public static string KEY_NOT_EXISTS<T>(T _key)
	{
		if (!Debug.isDebugBuild)
			return null;
		return "key " + _key +" not exists.";
	}

	public static string DO_NOTHING()
	{
		if (!Debug.isDebugBuild)
			return null;
		return "continue.";
	}

	public static string DO_RETURN()
	{
		if (!Debug.isDebugBuild)
			return null;
		return "return.";
	}
	public static string DO_RETURN<T>(T _val)
	{
		if (!Debug.isDebugBuild)
			return null;
		return "return " + _val + ".";
	}

	public static void Log(int _l, string _msg)
	{
		if (! Debug.isDebugBuild)
			return;

		switch (_l)
		{
			case 0: 
				Debug.Log(_msg);
				break;

			case 1:
				Debug.LogWarning(_msg);
				break;

			default:
				Debug.LogError(_msg);
				break;
		}
	}

	public static void Log(int _l, string _do, string _msg)
	{
		Log(_l, _do + _msg);
	}
}

