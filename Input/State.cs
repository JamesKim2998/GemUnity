
using UnityEngine;

namespace In
{
	public struct State
	{
		public bool isDown { get; private set; }
		public bool isUp { get; private set; }
		public bool isOn { get; private set; }
		public bool isOff { get { return !isOn; } }

		public void Down()
		{
			if (Debug.isDebugBuild)
			{
				if (isDown || !isUp || isOn)
					D.Log(1, D.DO_NOTHING(), D.INVALID_CALL());
			}

			isDown = true;
			isUp = false;
			isOn = true;
		}

		public void Up()
		{
			if (Debug.isDebugBuild)
			{
				if (!isDown || isUp || isOff)
					D.Log(1, D.DO_NOTHING(), D.INVALID_CALL());
			}

			isDown = false;
			isUp = true;
			isOn = false;
		}

		public void Tick()
		{
			if (Debug.isDebugBuild)
			{
				if (isOff)
					D.Log(1, D.DO_NOTHING(), D.INVALID_CALL());
			}

			isDown = isUp = false;
		}
	}
}