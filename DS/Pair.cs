namespace Gem
{
	public struct Pair<F, S>
	{
		public Pair(F _first, S _second)
		{
			first = _first;
			second = _second;
		}

		public F first;
		public S second;
	}
}