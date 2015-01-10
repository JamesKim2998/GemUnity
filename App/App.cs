namespace Gem
{
	public static class App
	{
		static App()
		{
			playing = true;
			sOnQuit.Conn(Broadcast.onQuit);
		}
		public static bool playing { get; private set; }

		private static readonly Connection sOnQuit = new Connection(delegate
		{
			playing = false;
		});
	}
}