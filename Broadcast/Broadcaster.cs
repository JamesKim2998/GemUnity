namespace Gem
{
	public class Broadcaster : Singleton<Broadcaster>
	{
		void OnApplicationQuit()
		{
			L.D("quit.");
			Broadcast.onQuit.val.CheckAndCall();
		}
	}
}