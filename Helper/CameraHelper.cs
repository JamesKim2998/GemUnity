using UnityEngine;

namespace Gem
{
	public static class CameraHelper 
	{
		public static Vector2 OrthoHSize(this Camera _this)
		{
			return new Vector2(_this.OrthoHW(), _this.OrthoHH());
		}

		public static float OrthoHW(this Camera _this)
		{
			return _this.aspect * _this.OrthoHH();
		}

		public static float OrthoHH(this Camera _this)
		{
			return _this.orthographicSize;
		}
	}
}