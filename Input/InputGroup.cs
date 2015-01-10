
using System.Collections.Generic;
using UnityEngine;

namespace Gem.In
{
	public class InputGroup
	{
		public InputGroup(InputManager _input)
		{
			mInput = _input;
		}

		~InputGroup()
		{
			if (App.playing)
				Unreg();
		}

		public void Reg()
		{
			if (isConn)
			{
				L.E(L.DO.RETURN_, L.M.CALL_INVALID);
				return;
			}

			isConn = true;

			foreach (var kv in mDict)
				mInput.Reg(kv.Key, kv.Value);
		}

		public void Unreg()
		{
			if (!isConn)
			{
				L.E(L.DO.RETURN_, L.M.CALL_INVALID);
				return;
			}

			isConn = false;

			foreach (var kv in mDict)
				mInput.Unreg(kv.Key, kv.Value);
		}

		public bool Add(InputCode _code, InputHandler _handler)
		{
			var _ret = mDict.TryAdd(_code, _handler);
			if (_ret && isConn) mInput.Reg(_code, _handler);
			return _ret;
		}

		public bool Remove(InputCode _code)
		{
			InputHandler _handler;
			var _ret = mDict.GetAndRemove(_code, out _handler);
			if (_ret && isConn) mInput.Unreg(_code, _handler);
			return _ret;
		}


		public bool isConn { get; private set; }
		private readonly InputManager mInput;
		private readonly Dictionary<InputCode, InputHandler> mDict = new Dictionary<InputCode, InputHandler>();
	}
}