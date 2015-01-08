using System;

namespace Gem
{

	public abstract class Arithmetic<T> : IArithmetic<T>
	{
		public abstract T val { get; set; }

		public abstract IArithmetic<T> Neg();

		public abstract IArithmetic<T> Add(IArithmetic<T>_other);

		public virtual IArithmetic<T> Sub(IArithmetic<T>_other)
		{
			return Add(_other.Neg());
		}

		public virtual IArithmetic<T> Inv()
		{
			throw new NotSupportedException();
		}

		public abstract IArithmetic<T> Mult(IArithmetic<T>_other);

		public virtual IArithmetic<T> Div(IArithmetic<T>_other)
		{
			return Mult(_other.Inv());
		}
	}

	public sealed class ArithmeticInt : Arithmetic<int>
	{
		public ArithmeticInt() { }

		public ArithmeticInt(int _val)
		{
			val = _val;
		}

		public override int val { get; set; }

		public override IArithmetic<int> Neg()
		{
			return new ArithmeticInt(-val);
		}

		public override IArithmetic<int> Add(IArithmetic<int> _other)
		{
			return new ArithmeticInt(val + _other.val);
		}

		public override IArithmetic<int> Mult(IArithmetic<int> _other)
		{
			return new ArithmeticInt(val * _other.val);
		}
	}

	public sealed class ArithmeticFloat : Arithmetic<float>
	{
		public ArithmeticFloat() { }

		public ArithmeticFloat(float _val)
		{
			val = _val;
		}

		public override float val { get; set; }

		public override IArithmetic<float> Neg()
		{
			return new ArithmeticFloat(-val);
		}

		public override IArithmetic<float> Add(IArithmetic<float> _other)
		{
			return new ArithmeticFloat(val + _other.val);
		}

		public override IArithmetic<float> Mult(IArithmetic<float> _other)
		{
			return new ArithmeticFloat(val * _other.val);
		}
	}
}