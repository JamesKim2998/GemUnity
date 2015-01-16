using System;
using System.Collections.Generic;
using System.Linq;

namespace Gem.In
{
	public enum InputMaskKey {}

	public partial class InputManager
	{
		private const int SIZE = InputCodeHelper.COUNT;

		public static readonly InputManager g = new InputManager();

		public readonly InputMap map = new InputMap();
		private readonly Binder[] mBinders = new Binder[SIZE];
		public InputMask mask { get; private set; }

		InputManager()
		{
			mask = new InputMask(0);
		}

		private Binder Binder_(InputCode _code)
		{
			return mBinders[(int)_code] ?? (mBinders[(int)_code] = new Binder());
		}

		public InputState State_(InputCode _code)
		{
			return map[_code];
		}

		public void Reg(InputCode _code, InputHandler _handler, PositionType _position = PositionType.BACK)
		{
			D.Assert(!IsDebugLocked());

			Binder_(_code).chain.Add(new HandlerState(_handler), _position);
		}

		public void Unreg(InputCode _code, InputHandler _handler)
		{
			D.Assert(!IsDebugLocked());

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

			if (! _binder.chain.Remove(_doCompare))
			{
				L.W(L.DO.RETURN_, L.M.KEY_NOT_EXISTS(_code));
				return;
			}

			if (_isListening)
			{
				if (!_binder.listeners.Remove(_data => (_data.handler == _handler)))
				{
					L.E(L.DO.NOTHING, L.M.KEY_NOT_EXISTS(_code));
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
			map.Tick();
			map.Fetch();
		}

		private static void PropagateDown(
			IEnumerable<HandlerState> _chain,
			ICollection<HandlerState> _listeners)
		{
			foreach (var _data in _chain.Reverse())
			{
				var _handler = _data.handler;
				if (!_handler.active) continue;
				if (_handler.block) break;

				if (_handler.down())
				{
					if (_handler.closed)
						_data.isOn = true;

					if (_handler.listen)
					{
						_data.isListening = true;
						_listeners.Add(_data);
					}

					if (_handler.swallow)
						break;
				}
			}
		}

		private static void PropagateUp(
			IEnumerable<HandlerState> _chain,
			ICollection<HandlerState> _listeners)
		{
			foreach (var _data in _chain.Reverse())
			{
				// opt
				// note: 새롭게 추가되는 상황을 고려합니다.
				//   o o x
				// x o o x
				if (!_data.isOn)
					continue;

				_data.isOn = false;
				_data.isListening = false;
				_data.handler.up();
			}

			_listeners.Clear();
		}

		private void Propagate() 
		{
			var _codeRaw = 0;

			foreach (var _binder in mBinders)
			{
				var _code = (InputCode) _codeRaw++;

				if (_binder == null)
					continue;

				if (mask[_code])
					continue;

				var _state = map[_code];
				var _chain = _binder.chain;
				var _listeners = _binder.listeners;

				if (_state.isDown)
				{
					L.D("propagate " + _code);
					if (_chain.Count == 0)
						L.W(L.M.HANDLE_NOT_EXIST(_code));
					else
						PropagateDown(_chain, _listeners);
				}
				else if (_state.isOn)
				{
					foreach (var _listener in _listeners.GetReverseEnum())
						_listener.handler.update();
				}
				else if (_state.isUp)
				{
					PropagateUp(_chain, _listeners);
				}
			}
		}

		private class HandlerState
		{
			public HandlerState(InputHandler _handler)
			{
				handler = _handler;
				isListening = false;
			}

			public readonly InputHandler handler;
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