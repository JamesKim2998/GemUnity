using UnityEngine;
using System.Collections;

public static class TransformHelper
{
	public static Vector2 DegToVector(float _degree) 
	{
		return new Vector2(
			Mathf.Cos(Mathf.Deg2Rad * _degree), 
			Mathf.Sin(Mathf.Deg2Rad * _degree));
	}

	public static float VectorToRad(Vector2 _vector)
	{
		return Mathf.Atan2(_vector.y, _vector.x);
	}

	public static float VectorToDeg(Vector2 _vector)
	{
		return Mathf.Rad2Deg * VectorToRad(_vector);
	}

	#region identity

	public static void SetParentIdentity(GameObject _child, GameObject _parent )
	{
		var _position = _child.transform.localPosition;
        var _rotation = _child.transform.localRotation;
        var _scale = _child.transform.localScale;
		_child.transform.parent = _parent.transform;
		_child.transform.localPosition = _position;
        _child.transform.localRotation = _rotation;
        _child.transform.localScale = _scale;
    }

	public static void SetParentIdentity<T>(GameObject _child, T _parent) where T : Component
	{
		SetParentIdentity(_child, _parent.gameObject);
	}

	public static void SetParentIdentity<T>(T _child, GameObject _parent) where T : Component
	{
		SetParentIdentity(_child.gameObject, _parent);
	}

	public static void SetParentIdentity<T1, T2>(T1 _child, T2 _parent)
		where T1 : Component
		where T2 : Component
	{
		SetParentIdentity(_child.gameObject, _parent.gameObject);
	}
	#endregion

	#region without scale
	public static void SetParentWithoutScale(GameObject _child, GameObject _parent )
	{
		var _scale = _child.transform.localScale;
		_child.transform.parent = _parent.transform;
		_child.transform.localScale = _scale;
	}

	public static void SetParentWithoutScale<T>(GameObject _child, T _parent) where T : Component
	{
		SetParentWithoutScale(_child, _parent.gameObject);
	}

	public static void SetParentWithoutScale<T>(T _child, GameObject _parent) where T : Component
	{
		SetParentWithoutScale(_child.gameObject, _parent);
	}

	public static void SetParentWithoutScale<T1, T2>(T1 _child, T2 _parent)
		where T1 : Component
		where T2 : Component
	{
		SetParentWithoutScale(_child.gameObject, _parent.gameObject);
	}
	#endregion
}

