using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LitJson;
using UnityEngine;

namespace Gem
{
	public static class JsonHelper
	{
		public static IEnumerable<JsonData> GetListEnum(this JsonData _data)
		{
			var _enum = ((IList)_data).GetEnumerator();
			while (_enum.MoveNext()) yield return (JsonData)_enum.Current;
		}

		public static IEnumerable<KeyValuePair<string, JsonData>> GetDictEnum(this JsonData _data)
		{
			return _data.Cast<KeyValuePair<string, JsonData>>();
		}

		public static bool TryGet(this JsonData _this, string _key, out string _val)
		{
			JsonData _data;

			if (_this.TryGet(_key, out _data))
			{
				if (_data.IsString)
				{
					_val = (string) _data;
					return true;
				}
				else
				{
					L.W(L.M.TYPE_WRONG(_data.GetJsonType(), default(string)));
				}
			}

			_val = string.Empty;
			return false;
		}

		public static bool TryGet(this JsonData _this, string _key, out bool _val)
		{
			JsonData _data;
			if (_this.TryGet(_key, out _data))
			{
				if (_data.IsBoolean)
				{
					_val = (bool) _data;
				}
				else
					L.W(L.M.TYPE_WRONG(_data.GetJsonType(), default(bool)));
			}

			_val = default(bool);
			return false;
		}

		public static bool TryGet(this JsonData _this, string _key, out int _val)
		{
			JsonData _data;
			if (_this.TryGet(_key, out _data))
			{
				if (_data.IsInt)
				{
					_val = (int) _data;
					return true;
				}
				else
					L.W(L.M.TYPE_WRONG(_data.GetJsonType(), default(int)));
			}

			_val = default(int);
			return false;
		}

		public static bool BoolOrDefault(this JsonData _this, string _key, bool _default = false)
		{
			bool _data;
			return _this.TryGet(_key, out _data) ? _data : _default;
		}

		public static int IntOrDefault(this JsonData _this, string _key, int _default = 0)
		{
			int _data;
			return _this.TryGet(_key, out _data) ? _data : _default;
		}

		public static string StringOrDefault(this JsonData _this, string _key, string _default = "")
		{
			string _data;
			return _this.TryGet(_key, out _data) ? _data : _default;
		}

		public static void AssignPrimitive(this JsonData _data, string _name, object _val)
		{
			var _type = _val.GetType();

			if (_type == typeof(bool))
				_data[_name] = (bool)_val;
			else if (_type == typeof(int))
				_data[_name] = (int)_val;
			else if (_type == typeof(double))
				_data[_name] = (double)_val;
			else if (_type == typeof(string))
				_data[_name] = (string)_val;
			else
				L.E(L.M.CASE_INVALID(_type));
		}

		public static string Key(this JsonReader _reader)
		{
			if (_reader.Token != JsonToken.PropertyName)
			{
				L.E(L.DO.RETURN_NULL, L.M.INVALID_STREAM);
				return null;
			}

			return (string)_reader.Value;
		}

		/// <summary>
		/// compare only process when debug
		/// </summary>
		public static bool AssertKey(this JsonReader _reader, string _cmp)
		{
			var _key = Key(_reader);
			if (_key == null) return false;

#if UNITY_EDITOR
			if (_key == _cmp)
				return true;
			L.E(L.DO.RETURN(false), L.M.KEY_NOT_EXISTS(_cmp));
#endif

			return false;
		}

		public static bool TryParse<T>(this JsonReader _reader, out T _ret)
		{
			_ret = default(T);

			if (_reader.Token != JsonToken.String)
			{
				L.E(L.DO.RETURN(false), L.M.TYPE_WRONG(_reader.Token, JsonToken.String));
				return false;
			}

			return EnumHelper.TryParse((string) _reader.Value, out _ret);
		}

		[Obsolete]
		public static JsonData DataWithRaw(Path_ _path)
		{
			var _fullPath = Raw.FullPath(_path);
			if (!_fullPath.HasValue) return null;
			return DataWithRaw(_fullPath.Value);
		}

		public static JsonData DataWithRaw(FullPath _path)
		{
			var s = Raw.Read(_path, FileMode.Open);
			if (s == null) return null;

#if UNITY_EDITOR
			try
			{
#endif
				return JsonMapper.ToObject(s);
#if UNITY_EDITOR
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				return null;
			}
#endif
		}

		public static bool ObjectWithData<T>(JsonData _data, out T _obj)
		{
			try
			{
				_obj = JsonMapper.ToObject<T>(_data.ToReader());
				return true;
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				_obj = default(T);
				return false;
			}
		}

		public static bool ObjectWithRaw<T>(FullPath _path, out T _obj)
		{
			var s = Raw.Read(_path, FileMode.Open);
			if (s == null)
			{
				_obj = default(T);
				return false;
			}

			using (s)
			using (TextReader _txt = new StreamReader(s.BaseStream))
			{
				try
				{
					_obj = JsonMapper.ToObject<T>(new JsonReader(_txt));
					return true;
				}
				catch (Exception e)
				{
					Debug.LogException(e);
					L.E("cannot read " + _path + " as json.");
					_obj = default(T);
					return false;
				}
			}
		}

		public static JsonReader ToReader(this JsonData _data)
		{
			// todo: optimize
			return new JsonReader(JsonMapper.ToJson(_data));
		}

		public static void StepOut(this JsonReader _reader)
		{
			var _depth = 0;

			while (_reader.Read())
			{
				switch (_reader.Token)
				{
					case JsonToken.ObjectStart:
						++_depth;
						break;
					case JsonToken.ObjectEnd:
						if (--_depth == -1) return;
						break;
				}
			}
		}

	}
}
