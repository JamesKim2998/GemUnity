using System;

namespace Gem
{

	public class Broadcast : Singleton<Broadcast>
	{
		public ActionWrap onQuit = new ActionWrap();

		void OnApplicationQuit()
		{
			onQuit.val.CheckAndCall();
		}
	}


}