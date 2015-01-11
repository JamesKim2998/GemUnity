using System.Collections.Generic;
using LitJson;

namespace Gem.In
{
	public class InputMap
	{
		private const int SIZE = (int) InputCode.END;

		private class FetcherAndState
		{
			public IInputFetcher fetcher { private get; set; }
			public InputState state;

			public FetcherAndState(InputState _state) 
			{
				state = _state;
			}

			public void TryFetch()
			{
				if (fetcher != null)
					fetcher.Fetch(ref state);
			}
		}

		private readonly FetcherAndState[] mMap = new FetcherAndState[SIZE];

		public InputMap()
		{
			for (var i = 0; i != SIZE; ++i)
				mMap[i] = new FetcherAndState(InputState.DEFAULT);
		}

		public InputState this[InputCode _code]
		{
			get { return mMap[(int) _code].state; }
		}

		public void Add(InputCode _code, IInputFetcher _fetcher) 
		{
			mMap[(int) _code].fetcher = _fetcher;
		}

		public void Load(JsonData _data)
		{
			foreach (var _kv in InputFetcherFactory.Read(_data))
				Add(_kv.Key, _kv.Value);
		}

		public void Fetch()
		{
			foreach (var _data in mMap)
				_data.TryFetch();
		}

		public void Tick()
		{
			foreach (var _data in mMap)
			{
				if (_data.state.isOn)
					_data.state.Tick();
			}
		}
	}


}