using System;
using System.Collections.Generic;

namespace Gem
{
	public class Timer
	{
		public enum Key {}

		private struct Alarm
		{
			public Alarm(Key _key, float _time, Action _act)
			{
				key = _key;
				time = _time;
				act = _act;
			}

			public readonly Key key;
			public readonly float time;
			public readonly Action act;
		}

		public float mElapsed;
		private Key mNextKey;

		private bool mIsSorted;
		private bool mIsUpdating;
		private readonly List<Alarm> mAlarms = new List<Alarm>();

		private readonly List<Alarm> mAddDirty = new List<Alarm>();
		private readonly List<Key> mRemvoeDirty = new List<Key>(); 

		public void Update(float _dt)
		{
			if (mAlarms.Empty())
				return;

			mElapsed += _dt;

			if (!mIsSorted)
				Sort();

			mIsUpdating = true;

			var _dead = -1;

			foreach (var _alarm in mAlarms)
			{
				if (_alarm.time > mElapsed)
					break;
				++_dead;
				_alarm.act();
			}

			if (_dead >= 0)
				mAlarms.RemoveRange(0, _dead + 1);

			mIsUpdating = false;

			mAlarms.AddRange(mAddDirty);
			mAddDirty.Clear();

			foreach (var _key in mRemvoeDirty)
				Remove(_key);
			mRemvoeDirty.Clear();
		}

		public Key Add(float _time, Action _act)
		{
			D.Assert(_act != null);

			var _alarm = new Alarm(mNextKey, _time + mElapsed, _act);

			if (!mIsUpdating)
			{
				mIsSorted = false;
				mAlarms.Add(_alarm);
			}
			else
			{
				mAddDirty.Add(_alarm);
			}

			return mNextKey++;
		}

		public void Remove(Key _key)
		{
			if (!mIsUpdating)
				mAlarms.RemoveIf(_alarm => _alarm.key == _key);
			else
				mRemvoeDirty.Add(_key);
		}

		private void Sort()
		{
			mIsSorted = true;
		}
	}

}