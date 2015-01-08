using System;
using UnityEngine;

namespace Gem
{
	public static class L
	{
		public static void Log(int _l, string _msg)
		{
			if (!App.d)
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
			Log(_l, _do + " " + _msg);
		}

		public static void D(string _msg) { Log(0, _msg); }
		public static void W(string _msg) { Log(1, _msg); }
		public static void E(string _msg) { Log(2, _msg); }

		public static void D(string _do, string _msg) { Log(0, _do, _msg); }
		public static void W(string _do, string _msg) { Log(1, _do, _msg); }
		public static void E(string _do, string _msg) { Log(2, _do, _msg); }

		public static string TYPE_WRONG_
		{
			get
			{
				return App.d
					? "type wrong." : null;
			}
		}

		public static string TYPE_WRONG<T, U>(T _has, U _expected)
		{
			if (App.d)
			{
				var _hasStr = typeof(T).Name + "(" + _has + ")";
				var _expectedStr = typeof(U).Name + "(" + _expected + ")";
				return _expectedStr + " expected but has " + _hasStr;
			}

			return null;
		}

		public static string ENUM_UNDEFINED<T>(T _type)
		{
			return App.d
				? "undefined enum " + _type + "."
				: null;
		}

		public static string ENUM_PARSE_FAIL<T>(string _str)
		{
			return App.d
				? "parse " + _str + " to enum " + typeof(T).Name + "failed."
				: null;
		}

		public static string CAST_FAIL
		{
			get
			{
				return App.d
					? "cast fail." : null;
			}
		}

		public static string CALL_RETRY(string _action)
		{
			return App.d
				? "trying to do " + _action + " again." : null;
		}

		public static string INVALID_CALL()
		{
			return App.d ? "invalid function call." : null;
		}

		public static string INVALID_STATE()
		{
			return App.d ? "invalid state." : null;
		}

		public static string SHOULD_NOT_NULL()
		{
			return App.d ? "value is null." : null;
		}

		public static string SHOULD_NOT_NULL<T>(T _val) where T : class
		{
			if (!App.d)
				return null;
			if (_val != null)
				throw new Exception("should not null always should be used with value null.");
			return typeof(T).Name + " is null.";
		}

		public static string SHOULD_NULL<T>(T _val) where T : class
		{
			if (!App.d)
				return null;
			if (_val == null)
				throw new Exception("should null always should be used with value.");
			return typeof(T).Name + " has value " + _val + ".";
		}

		public static string KEY_EXISTS<T>(T _key)
		{
			if (!App.d)
				return null;
			return "key " + _key + " already exists.";
		}

		public static string KEY_NOT_EXISTS<T>(T _key)
		{
			if (!App.d)
				return null;
			return "key " + _key + " not exists.";
		}

		public static string HANDLE_NOT_EXIST<T>(T _key)
		{
			return App.d
				? "no handler for " + _key + "(" + typeof(T).Name + ")"+ "."
				: null;
		}

		public static string INVALID_STREAM
		{
			get
			{
				return App.d
					? "invalid stream." : null;
			}
		}

		public static string RSC_NOT_EXISTS(string _file)
		{
			return App.d
				? "resource " + _file + " not exists."
				: null;
		}

		public static string DO_NOTHING()
		{
			if (!App.d)
				return null;
			return "continue.";
		}

		public static string DO_CONTINUE
		{
			get
			{
				return App.d
					? "continue." : null;
			}
		}

		public static string DO_RETURN_
		{
			get
			{
				return App.d
					? "return." : null;
			}
		}

		public static string DO_RETURN_NULL
		{
			get
			{
				return App.d
					? "return null." : null;
			}
		}

		[Obsolete]
		public static string DO_RETURN()
		{
			if (!App.d)
				return null;
			return "return.";
		}

		public static string DO_RETURN<T>(T _val)
		{
			if (!App.d)
				return null;
			return "return " + _val + ".";
		}

	}

}