using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MouseFollow))]
public class MouseActor : MonoBehaviour
{
	public LayerMask mask;
	public Func<GameObject, bool> act;

	void Update()
	{
		if (Input.GetMouseButtonUp(0))
			Act();
	}

	void Act()
	{
		var _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var _result = Physics2D.OverlapCircle(_mousePos, 0.1f, mask, -1, 1);

		if (!_result || (act == null)
			|| ! act(_result.gameObject))
		{
			Destroy(gameObject);
		}
	}
}