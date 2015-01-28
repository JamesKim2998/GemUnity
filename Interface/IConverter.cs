namespace Gem
{
	public interface IConverter<in I, out O>
	{
		O Convert(I _input);
	}

	public interface IBiConverter<A, B>
	{
		B Convert(A a);
		A Convert(B b);
	}
}