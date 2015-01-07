using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Debug = UnityEngine.Debug;

namespace Gem.In
{
	public enum InputFetcherType
	{
		KEY = 1,
		BTN = 2,
	}

	public static class InputFetcherFactory
	{
		public static IInputFetcher Make(JsonData _data)
		{
			InputFetcherType _type;
			if (! EnumHelper.TryParse((string) _data["type"], out _type))
				return null;

			var _reader = JsonHelper.ToReader(_data);

			switch (_type)
			{
				case InputFetcherType.KEY:
					return new InputFetcherKey(_reader);
				case InputFetcherType.BTN:
					return new InputFetcherButton(_reader);
				default:
					L.Log(2, L.DO_RETURN_NULL, L.ENUM_UNDEFINED(_type));
					return null;
			}
		}

		public static IEnumerable<KeyValuePair<InputCode, IInputFetcher>> Read(JsonData _data)
		{
			foreach (var _kv in JsonHelper.Dictionary(_data))
			{
				var _codeStr = _kv.Key;

				InputCode _code;
				if (! EnumHelper.TryParse(_codeStr, out _code))
				{
					L.Log(2, L.DO_RETURN_, L.INVALID_STREAM);
					yield break;
				}

				var _fetcher = Make(_kv.Value);
				if (_fetcher == null) continue;

				yield return new KeyValuePair<InputCode, IInputFetcher>(_code, _fetcher);
			}
		}
	}
}
