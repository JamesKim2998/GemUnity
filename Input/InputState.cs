using UnityEngine;

namespace Gem.In
{
	public struct InputState
	{
		public static readonly InputState DEFAULT = new InputState { isUp = true };
		private static readonly LevelLogger sLL = new LevelLogger(1);

		public bool isDown { get; private set; }
		public bool isUp { get; private set; }
		public bool isOn { get; private set; }
		public bool isOff { get { return !isOn; } }

		public void Down()
		{
			if (App.d)
			{
				sLL.Log(0, "down.");
				if (isDown || !isUp || isOn)
					sLL.Log(1, L.DO_NOTHING(), L.INVALID_CALL());
			}

			isDown = true;
			isUp = false;
			isOn = true;
		}

		public void Up()
		{
			if (App.d)
			{
				sLL.Log(0, "up.");
				if (isDown || isUp || isOff)
					sLL.Log(1, L.DO_NOTHING(), L.INVALID_CALL());
			}

			isDown = false;
			isUp = true;
			isOn = false;
		}

		public void Tick()
		{
			if (App.d)
			{
				if (isOff)
					sLL.Log(1, L.DO_NOTHING(), L.INVALID_CALL());
			}

			isDown = isUp = false;
		}
	}
}