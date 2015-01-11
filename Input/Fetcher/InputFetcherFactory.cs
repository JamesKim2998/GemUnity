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

			var _reader = _data.ToReader();

			switch (_type)
			{
				case InputFetcherType.KEY:
					return new InputFetcherKey(_reader);
				case InputFetcherType.BTN:
					return new InputFetcherButton(_reader);
				default:
					L.E(L.DO.RETURN_NULL, L.M.ENUM_UNDEFINED(_type));
					return null;
			}
		}

		public static IEnumerable<KeyValuePair<InputCode, IInputFetcher>> Read(JsonData _data)
		{
			foreach (var _kv in _data.GetEnumerable())
			{
				var _codeStr = _kv.Key;

				InputCode _code;
				if (! EnumHelper.TryParse(_codeStr, out _code))
				{
					L.E(L.DO.RETURN_, L.M.INVALID_STREAM);
					yield break;
				}

				var _fetcher = Make(_kv.Value);
				if (_fetcher == null) continue;

				yield return new KeyValuePair<InputCode, IInputFetcher>(_code, _fetcher);
			}
		}
	}
}
