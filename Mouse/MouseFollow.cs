using UnityEngine;
using System.Collections;

public class MouseFollow : MonoBehaviour {

	void Start()
	{
		Follow();
	}

	void Update () {
		Follow();
	}

	void Follow()
	{
		var _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var _posOld = transform.localPosition;
		_mousePos.z = _posOld.z;
		transform.position = _mousePos;
	}
}
