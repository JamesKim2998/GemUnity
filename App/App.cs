namespace Gem
{
	public static class App
	{
		public static bool playing { get; private set; }

		static App()
		{
			playing = true;
			Broadcast.onQuit += delegate { playing = false; };
		}
	}
}