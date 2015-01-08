namespace Gem
{
	public interface IArithmetic<T>
	{
		T val { get; set; }

		IArithmetic<T> Neg();
		IArithmetic<T> Add(IArithmetic<T> _other);
		IArithmetic<T> Sub(IArithmetic<T> _other);

		IArithmetic<T> Inv();
		IArithmetic<T> Mult(IArithmetic<T> _other);
		IArithmetic<T> Div(IArithmetic<T> _other);
	}

}