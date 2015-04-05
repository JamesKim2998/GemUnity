using System;

namespace Gem
{

	public static class Broadcast
	{
		public static Action onQuit;

		public static Action<int> onLevelWasLoaded;
	}

}