using UnityEngine;

namespace Gem
{

	public class DragAndDrop : MonoBehaviour
	{
		private Vector2 mOffset;
		public Vector2 offset
		{
			get { return mOffset; }
			set
			{
				mOffset = value;
				var _posOld = transform.position;
				var _posNew = (Vector3)mOffset + Camera.main.ScreenToWorldPoint(Input.mousePosition);
				_posNew.z = _posOld.z;
				transform.position = _posNew;
			}
		}

		#region physics
		private bool mPhysics = false;
		public bool physics
		{
			get { return mPhysics; }
			set
			{
				if (physics == value) return;
				mPhysics = value;
				if (physics)
					mVelocity = Vector2.zero;
			}
		}

		private Vector2 mVelocity;
		#endregion

		private bool mForcedStick = false;
		public void ForcedStick()
		{
			if (mForcedStick) return;
			mForcedStick = true;
			_OnMouseDown();
		}

		void Awake()
		{
			if (GetComponent<Rigidbody2D>())
				physics = true;
		}

		void Update()
		{
			if (mForcedStick)
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
			mVelocity = Vector2.Lerp(mVelocity, _velocity, 10 * Time.deltaTime);
		}

		protected virtual void _OnMouseUp()
		{
			mForcedStick = false;
			if (GetComponent<Rigidbody2D>())
				GetComponent<Rigidbody2D>().velocity = mVelocity;
		}
	}


}