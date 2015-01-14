using System;

namespace Gem
{

	public abstract class ConnectionBase<A, E> where E: class
	{
		protected ConnectionBase(A _act)
		{
			isConn = false;
			mAct = _act;
		}

		~ConnectionBase()
		{
			if (isConn) Dis();
		}

		public bool isConn { get; private set; }
		
		public void Conn(E _ev)
		{
			if (isConn)
			{
				L.W(L.DO.REPLACE("connection"), L.M.CALL_RETRY("connect"));
				Dis();
			}

			isConn = true;
			DoConn(_ev);
			mEv = new WeakReference(_ev);
		}

		public bool Dis()
		{
			if (!isConn)
			{
				L.W(L.DO.RETURN(false), L.M.CALL_RETRY("disconnect"));
				return false;
			}

			isConn = false;

			if (mEv.IsAlive)
			{
				DoDis(mEv.Target as E);
				mEv = null;
			}

			return true;
		}

		protected abstract void DoConn(E _ev);
		protected abstract void DoDis(E _ev);

		private WeakReference mEv;
		protected A mAct { get; private set; }
	}

	public sealed class Connection : ConnectionBase<Action, ActionWrap>
	{
		public Connection(Action _act) : base(_act)
		{}

		protected override void DoConn(ActionWrap _ev) { _ev.val += Fire; }
		protected override void DoDis(ActionWrap _ev) { _ev.val -= Fire; }
		private void Fire() { mAct.CheckAndCall(); }

	}

	public sealed class Connection<T> : ConnectionBase<Action<T>, ActionWrap<T>>
	{
		public Connection(Action<T> _act)
			: base(_act)
		{ }

		protected override void DoConn(ActionWrap<T> _ev) { _ev.val += Fire; }
		protected override void DoDis(ActionWrap<T> _ev) { _ev.val -= Fire; }
		private void Fire(T _param) { mAct.CheckAndCall(_param); }
	}

	public sealed class Connection<T1, T2> : ConnectionBase<Action<T1, T2>, ActionWrap<T1, T2>>
	{
		public Connection(Action<T1, T2> _act)
			: base(_act)
		{ }

		protected override void DoConn(ActionWrap<T1, T2> _ev) { _ev.val += Fire; }
		protected override void DoDis(ActionWrap<T1, T2> _ev) { _ev.val -= Fire; }
		private void Fire(T1 _param1, T2 _param2) { mAct.CheckAndCall(_param1, _param2); }
	}
}