using System;
using System.Collections.Generic;
using Gem;

// todo: memory allocation
namespace In
{
	public class Manager
	{
		private readonly HandlerState[] mStates = new HandlerState[(int) Code.END];

		private HandlerState HandlerState_(Code _code)
		{
			return mStates[(int) _code];
		}

		public State State_(Code _code)
		{
			return HandlerState_(_code).state;
		}

		public void Reg(Code _code, IHandler _handler, PositionType _position = PositionType.BACK)
		{
			ContainerHelper.Add(HandlerState_(_code).chain, new HandlerData(_handler), _position);
		}

		public void Unreg(Code _code, IHandler _handler)
		{
			var _isListening = false;
			var _handlerState = HandlerState_(_code);

			var _doCompare = new Predicate<HandlerData>(_data => {
				{
					if (_data.handler == _handler)
					{
						_isListening = _data.isListening;
						return true;
					}

					return false;
				}
			});

			if (! ContainerHelper.Remove(_handlerState.chain, _doCompare))
			{
				D.Log(1, D.DO_RETURN(), D.KEY_NOT_EXISTS(_code));
				return;
			}

			if (_isListening)
			{
				if (!ContainerHelper.Remove(_handlerState.listeners,
					_data => (_data.handler == _handler)))
				{
					D.Log(2, D.DO_NOTHING(), D.KEY_NOT_EXISTS(_code));
				}
			}
		}

		private struct UnregData
		{
			public Code code;
			public IHandler handler;
		}

		public void Update()
		{
			// todo: state Tick
			// todo: input map/state resolve

			var _unregs = new List<UnregData>();
			var _codeRaw = 0;

			foreach (var _handlerState in mStates)
			{
				var _code = (Code) _codeRaw++;

				if (_handlerState == null)
					continue;

				var _state = _handlerState.state;
				var _chain = _handlerState.chain;
				var _listeners = _handlerState.listeners;

				if (_state.isDown)
				{
					foreach (var _data in _chain)
					{
						var _handler = _data.handler;
						if (!_handler.active) continue;
						if (_handler.block) break;

						if (_handler.Down())
						{
							_data.isOn = true;
							if (_handler.shouldListen)
							{
								_data.isListening = true;
								_listeners.Add(_data);
							}
							if (_handler.swallow)
								break;
						}
					}
				}
				else if (_state.isOn)
				{
					foreach (var _listener in _listeners)
					{
						var _handler = _listener.handler;
						if (_handler.shouldUnreg)
							_unregs.Add(new UnregData() { code = _code, handler = _handler });
						else
							_handler.Update();
					}
				}
				else if (_state.isUp)
				{
					foreach (var _data in _chain)
					{
						// opt
						// note: 새롭게 추가되는 상황을 고려합니다.
						//   o o x
						// x o o x
						if (! _data.isOn)
							continue;

						_data.isOn = false;
						_data.isListening = false;
						_data.handler.Up();
					}

					_listeners.Clear();
				}
			}

			foreach (var _unreg in _unregs)
				Unreg(_unreg.code, _unreg.handler);
		}

		private class HandlerData
		{
			public HandlerData(IHandler _handler)
			{
				handler = _handler;
				isListening = false;
			}

			public IHandler handler;
			public bool isOn;
			public bool isListening;
		}

		private class HandlerState
		{
			public readonly State state = new State();
			public readonly LinkedList<HandlerData> chain = new LinkedList<HandlerData>();
			public readonly List<HandlerData> listeners = new List<HandlerData>();
		}
	}
}