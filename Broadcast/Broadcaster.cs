namespace Gem
{
	public class Broadcaster : Singleton<Broadcaster>
	{
		void OnApplicationQuit()
		{
			Broadcast.onQuit.CheckAndCall();
		}

		void OnLevelWasLoaded(int _level)
		{
			Broadcast.onLevelWasLoaded.CheckAndCall(_level);
		}
	}
}