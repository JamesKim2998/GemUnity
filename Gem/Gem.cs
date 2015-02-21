#pragma warning disable 0168

namespace Gem
{
	public class TheGem : Singleton<TheGem>
	{
		void Awake()
		{
			var _appAwake = App.playing;
			var _brAwake = Broadcaster.g;
		}
	}

}