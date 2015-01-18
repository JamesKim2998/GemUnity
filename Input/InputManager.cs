using System;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;

namespace Gem.In
{
	public enum InputMaskKey {}

	public partial class InputManager
	{
		private const int SIZE = InputCodeHelper.COUNT;

		public static readonly InputManager g = new InputManager();

		public readonly InputMap map = new InputMap();
		private readonly Binder[] mBinders = new Binder[SIZE];

		private bool mIsPropagating;

		private readonly List<InputBind> mUnregAfterTick
			= new List<InputBind>();

		InputManager()
		{}

		private Binder Binder_(InputCode _code)
		{
			return mBinders[(int)_code] ?? (mBinders[(int)_code] = new Binder());
		}

		public InputState State_(InputCode _code)
		{
			return map[_code];
		}

		public void Reg(InputBind _bind, PositionType _position = PositionType.BACK)
		{
			D.Assert(!IsDebugLocked());
			D.Assert(!mIsPropagating);
			Reg(_bind, (_position == PositionType.FRONT) 
				? InputPriority.FRONT : InputPriority.BACK);
		}

		public void Reg(InputBind _bind, InputPriority _priority)
		{
			Binder_(_bind.code).chain.Enqueue(new HandlerState(_bind.handler), (int) _priority);
		}

		public void Unreg(InputBind _bind)
		{
			D.Assert(!IsDebugLocked());
			D.Assert(!mIsPropagating);

			var _code = _bind.code;
			var _handler = _bind.handler;

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

			if (! _binder.chain.RemoveIf(_doCompare))
			{
				L.W(L.DO.RETURN_, L.M.KEY_NOT_EXISTS(_code));
				return;
			}

			if (_isListening)
			{
				if (!_binder.listeners.RemoveIf(_data => (_data.handler == _handler)))
				{
					L.E(L.DO.NOTHING, L.M.KEY_NOT_EXISTS(_code));
				}
			}
		}

		public void UnregAfterTick(InputBind _bind)
		{
			mUnregAfterTick.Add(_bind);
		}

		private void DoUnregAfterTick()
		{
			if (mUnregAfterTick.Empty())
				return;
			foreach (var _bind in mUnregAfterTick)
				Unreg(_bind);
			mUnregAfterTick.Clear();
		}

		public void Update()
		{
			DoUnregAfterTick();

			TickAndFetch();

			Propagate();

			DoUnregAfterTick();
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
			mIsPropagating = true;

			var _codeRaw = 0;

			foreach (var _binder in mBinders)
			{
				var _code = (InputCode) _codeRaw++;

				if (_binder == null)
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

			mIsPropagating = false;
		}

		private class HandlerState : PriorityQueueNode
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
			public readonly HeapPriorityQueue<HandlerState> chain = new HeapPriorityQueue<HandlerState>(8);
			public readonly List<HandlerState> listeners = new List<HandlerState>();
		}
	}
}