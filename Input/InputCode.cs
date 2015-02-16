namespace Gem
{
	public enum InputCode
	{
		U, D, L, R,

		/// <summary>
		/// yes. use/interact/confirm
		/// </summary>
		Y,
		/// <summary>
		/// no. cancel.
		/// </summary>
		N,

		/// <summary>
		/// page up
		/// </summary>
		PU,
		/// <summary>
		/// page down
		/// </summary>
		PD,

		B1,
		B2,
		B3,
		B4,

		/// <summary>
		/// toggle debug mode
		/// </summary>
		DEBUG,

		ESC, RET,
	}


	public static class InputCodeHelper
	{
		public const int COUNT = 15;

		public static InputCode ToInputCode(this Direction _dir)
		{
			D.Assert(EnumHelper.IsSingular(_dir));

			switch (_dir)
			{
			case Direction.U:
				return InputCode.U;
			case Direction.D:
				return InputCode.D;
			case Direction.L:
				return InputCode.L;
			case Direction.R:
				return InputCode.R;
			default:
				D.Assert(false);
				return default(InputCode);
			}
		}
	}
}