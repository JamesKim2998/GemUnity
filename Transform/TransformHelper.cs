using UnityEngine;

namespace Gem
{
	public static class TransformHelper
	{
		public static void SetEulerZ(this Transform _this, float _val)
		{
			var _euler = _this.localEulerAngles;
			_euler.z = _val;
			_this.localEulerAngles = _euler;
		}

		public static void SetParentIdentity(this Transform _this, Transform _parent)
		{
			var _pos = _this.localPosition;
			var _rot = _this.localRotation;
			var _scale = _this.localScale;
			_this.parent = _parent;
			_this.localPosition = _pos;
			_this.localRotation = _rot;
			_this.localScale = _scale;
		}

		public static void SetParentWithoutScale(this Transform _this, Transform _parent)
		{
			var _scale = _this.localScale;
			_this.parent = _parent.transform;
			_this.localScale = _scale;
		}
	}



}