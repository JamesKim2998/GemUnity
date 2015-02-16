namespace Gem
{
	public struct InputState
	{
		public static readonly InputState DEFAULT = new InputState { isUp = true };

		public bool isDown { get; private set; }
		public bool isUp { get; private set; }
		public bool isOn { get; private set; }
		public bool isOff { get { return !isOn; } }

		public void Down()
		{
#if UNITY_EDITOR
			L.D("down.");
			if (isDown || !isUp || isOn)
				L.W(L.DO.NOTHING, L.M.CALL_INVALID);
#endif

			isDown = true;
			isUp = false;
			isOn = true;
		}

		public void Up()
		{
#if UNITY_EDITOR
			L.D("up.");
			if (isDown || isUp || isOff)
				L.W(L.DO.NOTHING, L.M.CALL_INVALID);
#endif

			isDown = false;
			isUp = true;
			isOn = false;
		}

		public void Tick()
		{
#if UNITY_EDITOR
			if (isOff)
				L.W(L.DO.NOTHING, L.M.CALL_INVALID);
#endif

			isDown = isUp = false;
		}
	}
}