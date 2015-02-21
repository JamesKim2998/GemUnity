namespace Gem
{
	public static class PopupHelper
	{
		public static PopupKey MakeKey(string _name)
		{
			return (PopupKey)_name.GetHashCode();
		}
	}
}

