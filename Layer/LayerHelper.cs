using UnityEngine;
using System.Collections;

public static class LayerHelper
{
	public static bool Exist(LayerMask _mask, GameObject _target) 
	{
		return (_mask.value & (1 << _target.layer)) != 0;
	}

	public static bool Exist(LayerMask _mask, Collider2D _target) 
	{
		return Exist(_mask, _target.gameObject);
	}
}

