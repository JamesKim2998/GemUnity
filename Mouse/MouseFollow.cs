using Gem;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
	public new Camera camera;
	public Vector2 offset;

	void Start()
	{
		Follow();
	}

	void Update() 
	{
		Follow();
	}

	void Follow()
	{
		if (!camera) return;
		var _mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
		transform.SetPos((Vector2)_mousePos + offset);
	}
}
