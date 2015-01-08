using System;

namespace Gem.In
{
	public class InputHandler
	{
		public bool active = true;

		/// <summary>
		/// if true, input will be blocked before process down.
		/// </summary>
		public bool block = false;

		/// <summary>
		/// if true, input will not be propagated.
		/// </summary>
		public bool swallow = true;

		/// <summary>
		/// if true, update will be called.
		/// </summary>
		public bool listen = false;

		/// <summary>
		/// if true, up will be called.
		/// </summary>
		public bool closed = false;

		public bool forget { get { return listen && closed; } }

		/// <summary>
		/// only input with return value true will be processed.
		/// </summary>
		public Func<bool> down;
		public Action up;

		/// <summary>
		/// false will be unregistered.
		/// </summary>
		public Action update;
	}
}