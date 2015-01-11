#pragma warning disable 0162
#pragma warning disable 0429

using System;

namespace Gem
{
	public static partial class L
	{
		public static class M
		{
			#region type
			public static string TYPE_WRONG_
			{
				get
				{
					return d ? "type wrong." : null;
				}
			}

			public static string TYPE_WRONG<T, U>(T _has, U _expected)
			{
				if (d)
				{
					var _hasStr = typeof(T).Name + "(" + _has + ")";
					var _expectedStr = typeof(U).Name + "(" + _expected + ")";
					return _expectedStr + " expected but has " + _hasStr;
				}

				return null;
			}
			#endregion

			#region enum
			public static string ENUM_UNDEFINED<T>(T _type)
			{
				return d
					? "undefined enum " + _type + "."
					: null;
			}

			public static string ENUM_PARSE_FAIL<T>(string _str)
			{
				return d
					? "parse " + _str + " to enum " + typeof(T).Name + "failed."
					: null;
			}
			#endregion

			#region cast/parse/conversion
			public static string CAST_FAIL
			{
				get { return d ? "cast fail." : null; }
			}

			public static string PARSE_FAIL<T>(T _val)
			{
				return d ? "parse fail " + _val : null;
			}

			public static string CONV_NARROW 
			{
				get { return d ? "narrow conversion." : null; }
			}

			#endregion

			#region call
			public static string CALL_RETRY(string _action)
			{
				return d
					? "trying to do " + _action + " again." : null;
			}

			public static string CALL_INVALID
			{
				get
				{
					return d ? "invalid function call." : null;
				}
				
			}
			#endregion

			#region state
			public static string STATE_INVALID
			{
				get
				{
					return d ? "invalid state." : null;
				}
			}
			#endregion

			#region value
			public static string SHOULD_NOT_NULL_
			{
				get
				{
					return d ? "value is null." : null;
				}
			}

			public static string SHOULD_NOT_NULL(string _name)
			{
				return d ? (_name + " is null.") : null;
			}

			public static string SHOULD_NULL<T>(T _val) where T : class
			{
				if (!d)
					return null;
				if (_val == null)
					throw new Exception("should null always should be used with value.");
				return typeof(T).Name + " has value " + _val + ".";
			}

			public static string RANGE_INVALID<T>(T _val)
			{
				if (!d) return null;
				return _val + " range is invalid.";
			}
			#endregion

			#region switch/case
			public static string CASE_INVALID<T>(T _key)
			{
				return d ? "invalid case " + _key + "." : null;
			}
			#endregion

			#region enumerator
			public static string ENUMERATOR_INVALID
			{
				get { return d ? "enumerator invalid." : null; }
			}
			#endregion

			#region collection
			public static string KEY_EXISTS<T>(T _key)
			{
				if (!d)
					return null;
				return "key " + _key + " already exists.";
			}

			public static string KEY_NOT_EXISTS<T>(T _key)
			{
				if (!d)
					return null;
				return "key " + _key + " not exists.";
			}

			#endregion

			#region resource
			public static string RSC_NOT_EXISTS(string _file)
			{
				return d
					? "resource " + _file + " not exists."
					: null;
			}
			#endregion

			public static string HANDLE_NOT_EXIST<T>(T _key)
			{
				return d
					? "no handler for " + _key + "(" + typeof(T).Name + ")" + "."
					: null;
			}

			public static string INVALID_STREAM
			{
				get
				{
					return d ? "invalid stream." : null;
				}
			}

		}
	}
}