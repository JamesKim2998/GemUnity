using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gem
{
	using Func = Func<GameObject, float, TransformManipulator.Result>;

	public class TransformManipulator : MonoBehaviour
	{
		public enum Handle { }

		public struct Result
		{
			public static Result ID = new Result
			{
				pos = Vector2.zero, 
				rot = 1, 
				scale = Vector2.one
			};

			public Vector2 pos;
			public float rot;
			public Vector2 scale;
		}

		private struct Handler
		{
			public Handler(Handle _handle, Func _func)
			{
				handle = _handle;
				func = _func;
			}

			public readonly Handle handle;
			public readonly Func func;
		}

		private int mHandle;
		private readonly List<Handler> mOverrides = new List<Handler>();

		private List<Handler> mAdditives;
		private List<Handler> FetchAdditives()
		{
			return mAdditives ?? (mAdditives = new List<Handler>());
		}

		private static bool IsOverride(Handle _handle) { return (int) _handle%2 == 1; }

		private Handle FetchOverride() { return (Handle) (mHandle += mHandle % 2 + 1); }
		private Handle FetchAdditive() { return (Handle) (mHandle += (2 - mHandle % 2)); }

		public Handle AddOverride(Func _func)
		{
			var _handle = FetchOverride();
			mOverrides.Add(new Handler(_handle, _func));
			return _handle;
		}

		public Handle AddAdditive(Func _func)
		{
			var _handle = FetchAdditive();
			FetchAdditives().Add(new Handler(_handle, _func));
			return _handle;
		}

		public void Remove(Handle _handle)
		{
			if (IsOverride(_handle))
				mOverrides.RemoveIf(_handler => _handler.handle == _handle);
			else
				mAdditives.RemoveIf(_handler => _handler.handle == _handle);
		}

		public void Update()
		{
			if (mOverrides.Empty() && (mAdditives == null || mAdditives.Empty()))
				return;

			var _posOrg = transform.localPosition;
			var _pos = (Vector2)_posOrg;
			var _scaleOrg = transform.localScale;
			var _scale = (Vector2)_scaleOrg;
			var _rotOrg = transform.localRotation.z;
			var _rot = _rotOrg;

			var _dt = Time.deltaTime;

			if (!mOverrides.Empty())
			{
				var _result = mOverrides.Last().func(gameObject, _dt);
				_pos = _result.pos;
				_rot = _result.rot;
				_scale = _result.scale;
			}

			if (mAdditives != null)
			{
				foreach (var _handler in mAdditives)
				{
					var _result = _handler.func(gameObject, _dt);
					_pos += _result.pos;
					_rot += _result.rot;
					_scale = _scale.MultOTO(_result.scale);
				}
			}

			if (!_pos.InRadius(_posOrg))
				transform.localPosition = _pos;
			if (!_scale.InRadius(_scaleOrg))
				transform.localScale = _scale;
			if (!_rot.InRadius(_rotOrg))
				transform.SetEulerZ(_rot);
		}
	}

}