namespace Gem
{
	public static class PopupHelper
	{
		public static PopupKey MakeKey(string _name)
		{
			return (PopupKey)_name.GetHashCode();
		}

		private static readonly InputBind sBindClose = new InputBind(InputCode.ESC, 
			new InputHandler { down = () => ThePopup.Close(), });

		public static void RegKey()
		{
			InputManager.g.Reg(sBindClose);
		}

		public static void UnregKey()
		{
			InputManager.g.Unreg(sBindClose);
		}
	}
}

