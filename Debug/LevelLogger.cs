
namespace Gem
{
	public class LevelLogger
	{
		public int lv = 1;

		public LevelLogger() {}

		public LevelLogger(int _lv)
		{
			lv = _lv; 
		}

		public void Log(int _l, string _msg)
		{
			if (lv > _l) return;
			L.Log(_l, _msg);
		}

		public void Log(int _l, string _do, string _msg)
		{
			if (lv > _l) return;
			L.Log(_l, _do, _msg);
		}
	}

}