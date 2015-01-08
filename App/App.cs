using UnityEngine;

namespace Gem
{

	public static class App
	{
		static App()
		{
			d = Debug.isDebugBuild;
			playing = true;
			sOnQuit.Conn(Broadcast.g.onQuit);
		}

		public static bool d { get; private set; }

		public static bool playing { get; private set; }

		private static readonly Connection sOnQuit = new Connection(delegate
		{
			playing = false;
		});
	}


}