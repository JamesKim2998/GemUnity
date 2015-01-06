using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Gem
{
	public static class JsonHelper
	{
		public static JsonReader Resource(string _file)
		{
			var _text = RscHelper.Text(_file);
			if (_text == null) return null;
			return new JsonTextReader(new StringReader(_text.text));
		}

		public static string Key(JsonReader _reader)
		{
			if (_reader.TokenType != JsonToken.PropertyName)
			{
				D.Log(2, D.DO_RETURN_NULL, D.INVALID_STREAM);
				return null;
			}

			return (string) _reader.Value;
		}

		/// <summary>
		/// compare only process when debug
		/// </summary>
		public static bool AssertKey(JsonReader _reader, string _cmp)
		{
			var _key = Key(_reader);
			if (_key == null) return false;

			if (Debug.isDebugBuild)
			{
				if (_key == _cmp)
				{
					return true;
				}
				else
				{
					D.Log(2, D.DO_RETURN(false), D.KEY_NOT_EXISTS(_cmp));
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		public static void StepOut(JsonReader _reader)
		{
			var _depth = _reader.Depth;

			while (_reader.Read())
			{
				if (_depth > _reader.Depth)
					return;
			}
		}

		public static bool TryParse<T>(JsonReader _reader, out T _ret)
		{
			return EnumHelper.TryParse((string) _reader.Value, out _ret);
		}

		public static T Convert<T>(JsonReader _reader) where T : struct
		{
			return (T) new JsonSerializer().Deserialize(_reader, typeof(T));
		}

		public static string Convert<T>(T _data) where T : struct
		{
			return JsonConvert.SerializeObject(_data);
		}
	}
}
