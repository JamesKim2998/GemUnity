using System;
using Gem;
using UnityEngine;

public class MouseActor : MonoBehaviour
{
	public new Camera camera;
	public LayerMask mask;
	public Func<GameObject, bool> act;

	void Update()
	{
		if (Input.GetMouseButtonUp(0))
			Act();
	}

	void Act()
	{
		if (!camera)
		{
			L.D("act without camera.");
			return;
		}

		var _mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
		var _result = Physics2D.OverlapPoint(_mousePos, mask);

		if (!_result || (act == null)
			|| ! act(_result.gameObject))
		{
			Destroy(gameObject);
		}
	}
}