
using System.Collections.Generic;

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
			Unreg();
		}

		public void Reg()
		{
			if (isConn)
			{
				L.Log(2, L.DO_RETURN_, L.INVALID_CALL());
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
				L.Log(2, L.DO_RETURN_, L.INVALID_CALL());
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
		private InputManager mInput;
		private readonly Dictionary<InputCode, InputHandler> mDict = new Dictionary<InputCode, InputHandler>();
	}
}