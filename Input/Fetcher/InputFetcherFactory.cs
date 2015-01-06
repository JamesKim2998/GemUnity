using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gem.In
{
	public enum InputFetcherType
	{
		KEY = 1,
		BTN = 2,
	}

	public static class InputFetcherFactory
	{
		public static IInputFetcher Make(JsonReader _reader)
		{
			do
			{
				_reader.Read();

				if (!JsonHelper.AssertKey(_reader, "type"))
					break;

				InputFetcherType _type;

				_reader.Read();
				if (!JsonHelper.TryParse(_reader, out _type))
					break;

				switch (_type)
				{
					case InputFetcherType.KEY: return new InputFetcherKey(_reader);
					case InputFetcherType.BTN: return new InputFetcherButton(_reader);
				}
			} while (false);

			_reader.Skip();
			return null;
		}

		public struct InputCodeAndFetcher
		{
			public InputCode code;
			public IInputFetcher fetcher;
		}

		public static IEnumerable<InputCodeAndFetcher> Read(JsonReader _reader)
		{
			while (_reader.Read())
			{
				var _codeStr = JsonHelper.Key(_reader);
				if (_codeStr == null)
				{
					D.Log(2, D.DO_CONTINUE, D.INVALID_STREAM);
					_reader.Skip();
					continue;
				}

				InputCode _code;
				if (! EnumHelper.TryParse(_codeStr, out _code))
				{
					D.Log(2, D.DO_CONTINUE, D.INVALID_STREAM);
					_reader.Skip();
					continue;
				}

				var _fetcher = Make(_reader);
				if (_fetcher == null) continue;
				
				yield return new InputCodeAndFetcher{ code = _code, fetcher = _fetcher };
			}
		}
	}
}
