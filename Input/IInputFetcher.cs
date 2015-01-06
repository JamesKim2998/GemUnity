namespace Gem.In
{
	public interface IInputFetcher
	{
		InputState Fetch(InputState _old);
	}
}