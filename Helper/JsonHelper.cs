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
		public static IEnumerable<KeyValuePair<string, JsonData>> Dictionary(this JsonData _data)
		{
			return _data.Cast<KeyValuePair<string, JsonData>>();
		}

		public static string Key(this JsonReader _reader)
		{
			if (_reader.Token != JsonToken.PropertyName)
			{
				L.Log(2, L.DO_RETURN_NULL, L.INVALID_STREAM);
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

			if (Debug.isDebugBuild)
			{
				if (_key == _cmp)
					return true;
				L.Log(2, L.DO_RETURN(false), L.KEY_NOT_EXISTS(_cmp));
				return false;
			}

			return false;
		}

		public static bool TryParse<T>(this JsonReader _reader, out T _ret)
		{
			_ret = default(T);

			if (_reader.Token != JsonToken.String)
			{
				L.Log(2, L.DO_RETURN(false), L.TYPE_WRONG(_reader.Token, JsonToken.String));
				return false;
			}

			return EnumHelper.TryParse((string) _reader.Value, out _ret);
		}

		public static JsonData DataWithFile(string _file)
		{
			var _text = RscHelper.Text(_file);
			if (_text == null) return null;
			return JsonMapper.ToObject(_text.text);
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
