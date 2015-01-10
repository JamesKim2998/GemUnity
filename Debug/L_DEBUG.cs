#if UNITY_EDITOR
using System.Collections.Generic;
using Debug = UnityEngine.Debug;

namespace Gem
{
	public static partial class L
	{
		public const V globalLevel = V.W;

		private readonly static Dictionary<ClassID, V> sVMap = new Dictionary<ClassID, V>();

		private static V GetLevel(ClassID _classID)
		{
			V _v;
			return sVMap.TryGetValue(_classID, out _v) 
				? _v : globalLevel;
		}

		public static void SetLevel(V _v)
		{
			var _caller = new CallerInfo(1);
			var _id = _caller.id;
			if (sVMap.ContainsKey(_id))
				sVMap[_id] = _v;
		}

		private static void Log(int _depth, V _v, string _msg)
		{
			var _caller = new CallerInfo(_depth + 1);

			if (GetLevel(_caller.id) >  _v)
				return;

			var _callerMsg = "undefined";
			if (_caller.id != 0)
				_callerMsg = _caller.descript;

			_msg = _callerMsg + ": " + _msg;


			switch (_v)
			{
				case V.N:
					break;

				case V.D:
					Debug.Log(_msg);
					break;

				case V.W:
					Debug.LogWarning(_msg);
					break;

				case V.E:
					Debug.LogError(_msg);
					break;

				default:
					Debug.LogError(_msg);
					break;
			}
		}

		private static void Log(int _depth, V _v, string _do, string _msg)
		{
			Log(_depth + 1, _v, _do + " " + _msg);
		}

		public static void D(string _msg) { Log(1, V.D, _msg); }
		public static void W(string _msg) { Log(1, V.W, _msg); }
		public static void E(string _msg) { Log(1, V.E, _msg); }

		public static void D(string _do, string _msg) { Log(1, V.D, _do, _msg); }
		public static void W(string _do, string _msg) { Log(1, V.W, _do, _msg); }
		public static void E(string _do, string _msg) { Log(1, V.E, _do, _msg); }

	}
}

#endif