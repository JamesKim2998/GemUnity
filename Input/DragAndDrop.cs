using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour
{
	private Vector2 m_Offset;
	public Vector2 offset
	{
		get { return m_Offset; }
		set
		{
			m_Offset = value;
			var _posOld = transform.position;
			var _posNew = (Vector3) m_Offset + Camera.main.ScreenToWorldPoint(Input.mousePosition);
			_posNew.z = _posOld.z;
			transform.position = _posNew;
		}
	}

	#region physics
	private bool m_Physics = false;
	public bool physics
	{
		get { return m_Physics; }
		set
		{
			if (physics == value) return;
			m_Physics = value;
			if (physics)
				m_Velocity = Vector2.zero;
		}
	}

	private Vector2 m_Velocity;
	#endregion

	private bool m_ForcedStick = false;
	public void ForcedStick()
	{
		if (m_ForcedStick) return;
		m_ForcedStick = true;
		_OnMouseDown();
	}

	void Awake()
	{
		if (rigidbody2D) 
			physics = true;
	}

	void Update()
	{
		if (m_ForcedStick)
		{
			if (Input.GetMouseButtonDown(0)) 
				_OnMouseDown();
			else if (Input.GetMouseButton(0))
				_OnMouseDrag();
			else if (Input.GetMouseButtonUp(0))
				_OnMouseUp();
		}
	}

	void OnMouseDown() { _OnMouseDown(); }
	void OnMouseDrag() { _OnMouseDrag(); }
	void OnMouseUp() { _OnMouseUp(); }

	protected virtual void _OnMouseDown()
	{
		offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	protected virtual void _OnMouseDrag()
	{
		var _positionOld = transform.position;
		var _position = _positionOld;
		var _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		_position.x = _mousePosition.x;
		_position.y = _mousePosition.y;
		transform.position = _position + (Vector3)offset;

		var _velocity = ((Vector2)transform.position - (Vector2)_positionOld) / Time.deltaTime;
		m_Velocity = Vector2.Lerp(m_Velocity, _velocity, 10 * Time.deltaTime);
	}

	protected virtual void _OnMouseUp()
	{
		m_ForcedStick = false; 
		if (rigidbody2D)
			rigidbody2D.velocity = m_Velocity;
	}
}
