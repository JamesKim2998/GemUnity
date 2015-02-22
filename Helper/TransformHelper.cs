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

		public static void SetPosX(this Transform _this, float _val)
		{
			var _pos = _this.position;
			_pos.x = _val;
			_this.position = _pos;
		}

		public static void SetPosY(this Transform _this, float _val)
		{
			var _pos = _this.position;
			_pos.y = _val;
			_this.position = _pos;
		}

		public static void SetPosZ(this Transform _this, float _val)
		{
			var _pos = _this.position;
			_pos.z = _val;
			_this.position = _pos;
		}

		public static void SetLPosX(this Transform _this, float _val)
		{
			var _pos = _this.localPosition;
			_pos.x = _val;
			_this.localPosition = _pos;
		}

		public static void SetLPosY(this Transform _this, float _val)
		{
			var _pos = _this.localPosition;
			_pos.y = _val;
			_this.localPosition = _pos;
		}

		public static void SetLPosZ(this Transform _this, float _val)
		{
			var _pos = _this.localPosition;
			_pos.z = _val;
			_this.localPosition = _pos;
		}

		public static void SetEulerZ(this Transform _this, float _val)
		{
			var _euler = _this.eulerAngles;
			_euler.z = _val;
			_this.eulerAngles = _euler;
		}

		public static void SetLEulerZ(this Transform _this, float _val)
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

		public static Vector2 Size(this RectTransform _this)
		{
			return _this.rect.size;
		}

		public static float W(this RectTransform _this)
		{
			return _this.rect.width;
		}
		public static float H(this RectTransform _this)
		{
			return _this.rect.height;
		}

		public static float MinX(this RectTransform _this)
		{
			return _this.offsetMin.x;
		}

		public static float MaxX(this RectTransform _this)
		{
			return _this.offsetMax.x;
		}

		public static float MinY(this RectTransform _this)
		{
			return _this.offsetMin.y;
		}

		public static float MaxY(this RectTransform _this)
		{
			return _this.offsetMax.y;
		}

		public static void SetPivotX(this RectTransform _this, float _val)
		{
			var _pivot = _this.pivot;
			_pivot.x = _val;
			_this.pivot = _pivot;
		}

		public static void SetPivotY(this RectTransform _this, float _val)
		{
			var _pivot = _this.pivot;
			_pivot.y = _val;
			_this.pivot = _pivot;
		}

		public static void SetPivotAndAnchors(this RectTransform _this, Vector2 _vec)
		{
			_this.pivot = _vec;
			_this.anchorMin = _vec;
			_this.anchorMax = _vec;
		}

		public static void SetSize(this RectTransform _this, Vector2 _size)
		{
			var _sizeOld = _this.rect.size;
			var _sizeDelta = _size - _sizeOld;
			_this.offsetMin = _this.offsetMin - new Vector2(_sizeDelta.x * _this.pivot.x, _sizeDelta.y * _this.pivot.y);
			_this.offsetMax = _this.offsetMax + new Vector2(_sizeDelta.x * (1f - _this.pivot.x), _sizeDelta.y * (1f - _this.pivot.y));
		}

		public static void SetW(this RectTransform _this, float _size)
		{
			SetSize(_this, new Vector2(_size, _this.rect.size.y));
		}

		public static void SetH(this RectTransform _this, float _size)
		{
			SetSize(_this, new Vector2(_this.rect.size.x, _size));
		}

		public static void SetMinX(this RectTransform _this, float _val)
		{
			var o = _this.offsetMin;
			o.x = _val;
			_this.offsetMin = o;
		}

		public static void SetMaxX(this RectTransform _this, float _val)
		{
			var o = _this.offsetMax;
			o.x = _val;
			_this.offsetMax = o;
		}

		public static void SetMinY(this RectTransform _this, float _val)
		{
			var o = _this.offsetMin;
			o.y = _val;
			_this.offsetMin = o;
		}

		public static void SetMaxY(this RectTransform _this, float _val)
		{
			var o = _this.offsetMax;
			o.y = _val;
			_this.offsetMax = o;
		}

		public static void SetParentIdentity(this RectTransform _this, RectTransform _parent)
		{
			var _oldLocalScale = _this.localScale;
			var _oldPivot = _this.pivot;
			var _oldOffsetMax = _this.offsetMax;
			var _oldOffsetMin = _this.offsetMin;

			_this.SetParent(_parent);
			_this.localScale = _oldLocalScale;
			_this.pivot = _oldPivot;
			_this.offsetMax = _oldOffsetMax;
			_this.offsetMin = _oldOffsetMin;
		}

		public static void Fill(this RectTransform _this)
		{
			_this.anchorMin = Vector2.zero;
			_this.anchorMax = Vector2.one;
			_this.offsetMin = Vector2.zero;
			_this.offsetMax = Vector2.zero;
		}
	}

}