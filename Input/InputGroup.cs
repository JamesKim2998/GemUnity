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
			if (App.playing)
				Unreg();
		}

		public int count { get { return mBinds.Count; } }

		public void Reg()
		{
			if (isConn)
			{
				L.E(L.DO.RETURN_, L.M.CALL_INVALID);
				return;
			}

			isConn = true;

			foreach (var _bind in mBinds)
				mInput.Reg(_bind);
		}

		public void Unreg()
		{
			if (!isConn)
			{
				L.E(L.DO.RETURN_, L.M.CALL_INVALID);
				return;
			}

			isConn = false;

			foreach (var _bind in mBinds)
				mInput.Unreg(_bind);
		}

		public void Add(InputBind _bind)
		{
			mBinds.Add(_bind);
			if (isConn) mInput.Reg(_bind);
		}

		public void Remove(InputBind _bind)
		{
			mBinds.Remove(_bind);
			if (isConn) mInput.Unreg(_bind);
		}

		public bool isConn { get; private set; }
		private readonly InputManager mInput;
		private readonly List<InputBind> mBinds = new List<InputBind>();
	}
}