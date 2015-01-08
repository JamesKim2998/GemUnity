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
		
		public bool Conn(E _ev)
		{
			if (isConn)
			{
				L.W(L.DO_RETURN(false), L.CALL_RETRY("connect"));
				return false;
			}

			isConn = true;
			DoConn(_ev);
			mEv = new WeakReference(_ev);

			return true;
		}

		public bool Dis()
		{
			if (!isConn)
			{
				L.W(L.DO_RETURN(false), L.CALL_RETRY("disconnect"));
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
		private void Fire(T _param) { mAct.CheckAndCall(ref _param); }
	}
}