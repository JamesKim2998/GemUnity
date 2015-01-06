namespace Gem.In
{
	class InputMap
	{
		private const int SIZE = (int) InputCode.END;

		private struct FetcherAndState
		{
			public IInputFetcher fetcher { private get; set; }
			public InputState state { get; private set; }

			public FetcherAndState(InputState _state) : this()
			{
				state = _state;
			}

			public void TryFetch()
			{
				if (fetcher != null)
					state = fetcher.Fetch(state);
			}
		}

		private readonly FetcherAndState[] mMap = new FetcherAndState[SIZE];

		public InputMap()
		{
			for (var i = 0; i != SIZE; ++i)
				mMap[i] = new FetcherAndState(new InputState());
		}

		public InputState this[InputCode _code]
		{
			get { return mMap[(int) _code].state; }
		}

		public void SetFetcher(InputCode _code, IInputFetcher _fetcher) 
		{
			mMap[(int) _code].fetcher = _fetcher;
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