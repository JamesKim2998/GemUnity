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
			public Vector2? pos;
			public float? rot;
			public Vector2? scale;
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

		public Func default_;

		private readonly List<Handler> mOverrides = new List<Handler>();

		private List<Handler> mAdditives;
		private List<Handler> FetchAdditives()
		{
			return mAdditives ?? (mAdditives = new List<Handler>());
		}

		private static bool IsOverride(Handle _handle) { return (int)_handle % 2 == 1; }

		private Handle FetchOverride() { return (Handle)(mHandle += mHandle % 2 + 1); }
		private Handle FetchAdditive() { return (Handle)(mHandle += (2 - mHandle % 2)); }

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
			if (default_ == null && mOverrides.Empty()
				&& (mAdditives == null || mAdditives.Empty()))
			{
				return;
			}

			Result _result;
			var _dt = Time.deltaTime;

			if (!mOverrides.Empty())
				_result = mOverrides.Last().func(gameObject, _dt);
			else if (default_ != null)
				_result = default_(gameObject, _dt);
			else
				_result = new Result();

			if (mAdditives != null)
			{
				foreach (var _handler in mAdditives)
				{
					var _add = _handler.func(gameObject, _dt);

					if (_add.pos != null)
					{
						if (_result.pos == null)
							_result.pos = transform.position;
						_result.pos += _add.pos;
					}

					if (_add.rot != null)
					{
						if (_result.rot == null)
							_result.rot = transform.rotation.z;
						_result.rot += _add.rot;
					}

					if (_add.scale != null)
					{
						if (_result.scale == null)
							_result.scale = transform.localScale;
						_result.scale = _result.scale.Value.MultOTO(_add.scale.Value);
					}
				}
			}

			if (_result.pos != null)
				transform.position = _result.pos.Value;
			if (_result.rot != null)
				transform.SetEulerZ(_result.rot.Value);

			// note: scale만 local 값을 사용합니다.
			if (_result.scale != null)
				transform.localScale = _result.scale.Value;
		}
	}

}