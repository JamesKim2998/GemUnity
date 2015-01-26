using UnityEngine;

namespace Gem
{
	public static class TransformHelper
	{
		public static void SetPos(this Transform _this, Vector2 _val)
		{
			var _pos = _this.position;
			_pos.x = _val.x;
			_pos.y = _val.y;
			_this.position = _pos;
		}

		public static void SetPosY(this Transform _this, float _val)
		{
			var _pos = _this.position;
			_pos.y = _val;
			_this.position = _pos;
		}

		public static void SetEulerZ(this Transform _this, float _val)
		{
			var _euler = _this.eulerAngles;
			_euler.z = _val;
			_this.eulerAngles = _euler;
		}

		public static void SetLocalEulerZ(this Transform _this, float _val)
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

		public static void CastRectAndAssignWith(this Transform _this, Transform _other)
		{
			((RectTransform)_this.transform).AssignWith((RectTransform)_other.transform);
		}

		public static void AssignWith(this RectTransform _this, RectTransform _other)
		{
			_this.offsetMin = _other.offsetMin;
			_this.offsetMax = _other.offsetMax;
		}
	}

}