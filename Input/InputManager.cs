using System;
using System.Collections.Generic;

namespace Gem.In
{
	public class InputManager
	{
		private readonly InputMap mMap = new InputMap();
		private readonly Binder[] mBinders = new Binder[(int) InputCode.END];

		private Binder Binder_(InputCode _code)
		{
			return mBinders[(int)_code] ?? (mBinders[(int)_code] = new Binder());
		}

		public InputState State_(InputCode _code)
		{
			return mMap[_code];
		}

		public void Reg(InputCode _code, IInputHandler _handler, PositionType _position = PositionType.BACK)
		{
			ContainerHelper.Add(Binder_(_code).chain, new HandlerState(_handler), _position);
		}

		public void Unreg(InputCode _code, IInputHandler _handler)
		{
			var _isListening = false;
			var _binder = Binder_(_code);

			var _doCompare = new Predicate<HandlerState>(_data => {
				{
					if (_data.handler == _handler)
					{
						_isListening = _data.isListening;
						return true;
					}

					return false;
				}
			});

			if (! ContainerHelper.Remove(_binder.chain, _doCompare))
			{
				D.Log(1, D.DO_RETURN_, D.KEY_NOT_EXISTS(_code));
				return;
			}

			if (_isListening)
			{
				if (!ContainerHelper.Remove(_binder.listeners,
					_data => (_data.handler == _handler)))
				{
					D.Log(2, D.DO_NOTHING(), D.KEY_NOT_EXISTS(_code));
				}
			}
		}

		public void Update()
		{
			TickAndFetch();
			Propagate();
		}

		private void TickAndFetch()
		{
			mMap.Tick();
			mMap.Fetch();
		}

		private static void PropagateDown(
			IEnumerable<HandlerState> _chain,
			ICollection<HandlerState> _listeners)
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

		private struct UnregData
		{
			public InputCode code;
			public IInputHandler handler;
		}

		private static void PropagateOn(
			InputCode _code,
			IEnumerable<HandlerState> _listeners,
			ICollection<UnregData> _unregs)
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

		private static void PropagateUp(
			IEnumerable<HandlerState> _chain,
			ICollection<HandlerState> _listeners)
		{
			foreach (var _data in _chain)
			{
				// opt
				// note: 새롭게 추가되는 상황을 고려합니다.
				//   o o x
				// x o o x
				if (!_data.isOn)
					continue;

				_data.isOn = false;
				_data.isListening = false;
				_data.handler.Up();
			}

			_listeners.Clear();
		}

		private void Propagate() 
		{
			var _unregs = new List<UnregData>();
			var _codeRaw = 0;

			foreach (var _binder in mBinders)
			{
				var _code = (InputCode) _codeRaw++;

				if (_binder == null)
					continue;

				var _state = mMap[_code];
				var _chain = _binder.chain;
				var _listeners = _binder.listeners;

				if (_state.isDown)
				{
					PropagateDown(_chain, _listeners);
				}
				else if (_state.isOn)
				{
					PropagateOn(_code, _listeners, _unregs);
				}
				else if (_state.isUp)
				{
					PropagateUp(_chain, _listeners);
				}
			}

			foreach (var _unreg in _unregs)
				Unreg(_unreg.code, _unreg.handler);
		}

		private class HandlerState
		{
			public HandlerState(IInputHandler _handler)
			{
				handler = _handler;
				isListening = false;
			}

			public readonly IInputHandler handler;
			public bool isOn;
			public bool isListening;
		}

		private class Binder
		{
			public readonly LinkedList<HandlerState> chain = new LinkedList<HandlerState>();
			public readonly List<HandlerState> listeners = new List<HandlerState>();
		}
	}
}