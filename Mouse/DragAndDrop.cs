using UnityEngine;

namespace Gem
{
	public class DragAndDrop : MonoBehaviour
	{
		private Camera mCamera;
		public new Camera camera
		{
			get
			{
				if (mCamera)
					return mCamera;
				else
					return Camera.main;
			}

			set { mCamera = value; }
		}

		private Vector2 mOffset;
		public Vector2 offset
		{
			get { return mOffset; }
			set
			{
				mOffset = value;
				var _posOld = transform.position;
				var _posNew = (Vector3)mOffset + camera.ScreenToWorldPoint(Input.mousePosition);
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
			DoMouseDown();
		}

		void Awake()
		{
			if (GetComponent<Rigidbody2D>())
				physics = true;
		}

		protected virtual void Start()
		{}

		void Update()
		{
			if (mForcedStick)
			{
				if (Input.GetMouseButtonDown(0))
					DoMouseDown();
				else if (Input.GetMouseButtonUp(0))
					DoMouseUp();
				else
					DoMouseDrag();
			}
		}

		void OnMouseDown() { DoMouseDown(); }
		void OnMouseDrag() { DoMouseDrag(); }
		void OnMouseUp() { DoMouseUp(); }

		protected virtual void DoMouseDown()
		{
			offset = transform.position - camera.ScreenToWorldPoint(Input.mousePosition);
		}

		protected virtual void DoMouseDrag()
		{
			var _positionOld = transform.position;
			var _position = _positionOld;
			var _mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
			_position.x = _mousePosition.x;
			_position.y = _mousePosition.y;
			transform.position = _position + (Vector3)offset;

			var _velocity = ((Vector2)transform.position - (Vector2)_positionOld) / Time.deltaTime;
			mVelocity = Vector2.Lerp(mVelocity, _velocity, 10 * Time.deltaTime);
		}

		protected virtual void DoMouseUp()
		{
			mForcedStick = false;
			if (GetComponent<Rigidbody2D>())
				GetComponent<Rigidbody2D>().velocity = mVelocity;
		}
	}


}