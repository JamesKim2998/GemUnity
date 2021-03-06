﻿using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Gem
{
	public static class JsonHelper2
	{
		public static bool TryGetAndParse<T>(this JObject _this, string _key, out T _value)
		{
			JToken _member;
			if (!_this.TryGetValue(_key, out _member))
			{
				L.W("member " + _key + " not found.");
				_value = default(T);
				return false;
			}

			if (_member.Type != JTokenType.String)
			{
				L.W("member " + _key + " found. but not string, " + _member.Type);
				_value = default(T);
				return false;
			}

			return EnumHelper.TryParse((string)_member, out _value);
		}

		public static bool ObjectWithData<T>(JObject _data, out T _obj)
		{
			try
			{
				var _serializer = new JsonSerializer();
				_obj = _serializer.Deserialize<T>(_data.CreateReader());
				return true;
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				_obj = default(T);
				return false;
			}
		}

		public static bool Deserialize<T>(Path _path, out T _obj)
		{
			var s = Raw.Read(_path, FileMode.Open);
			if (s == null)
			{
				_obj = default(T);
				return false;
			}

			using (s)
			using (var _txt = new StreamReader(s.BaseStream))
			using (var _reader  = new JsonTextReader(_txt))
			{
				try
				{
					var _serializer = new JsonSerializer();
					_obj = _serializer.Deserialize<T>(_reader);
					return true;
				}
				catch (Exception e)
				{
					Debug.LogException(e);
					_obj = default(T);
					return false;
				}
			}
		}

		public static bool Serialize<T>(Path _path, T _obj)
		{
			try
			{
				using (var _file = File.CreateText(_path))
				using (var _writer = new JsonTextWriter(_file))
				{
					new JsonSerializer().Serialize(_writer, _obj);
					return true;
				}	
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				return false;
			}
		}
	}
}
