using System;

namespace In
{
	public abstract class IHandler
	{
		public virtual bool active { get { return false; } }

		public virtual bool block { get { return false; } }

		public virtual bool swallow { get { return false; } }

		/// <summary>
		/// only input with return value true will be processed
		/// </summary>
		public abstract bool Down();

		public abstract void Up();

		public virtual bool shouldListen { get { return false; } }
		public virtual bool shouldUnreg { get { return false; } }

		/// <summary>
		/// false will be unregistered.
		/// </summary>
		public virtual void Update()
		{
			throw new NotSupportedException();
		}
	}
}