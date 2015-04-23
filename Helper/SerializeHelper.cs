using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Gem
{
	public static class SerializeHelper
	{
		public static MemoryStream Enc(object o)
		{
			var s = new MemoryStream();
			var _formatter = new BinaryFormatter();
			_formatter.Serialize(s, o);
			return s;
		}

		public static void Enc(object o, Path _path)
		{
			using (var s = Enc(o))
			using (var f = new FileStream(_path, FileMode.Create, FileAccess.Write))
				s.WriteTo(f);
		}

		public static T Dec<T>(MemoryStream s)
		{
			IFormatter _formatter = new BinaryFormatter();
			s.Seek(0, SeekOrigin.Begin);
			return (T) _formatter.Deserialize(s);
		}

		public static bool Dec<T>(Path _path, out T _data)
		{
			using (var s = new MemoryStream())
			{
				try
				{
					using (var f = new FileStream(_path, FileMode.Open, FileAccess.Read))
					{
						var _bytes = new byte[f.Length];
						f.Read(_bytes, 0, (int) f.Length);
						s.Write(_bytes, 0, (int) f.Length);
					}

				}
				catch (Exception _e)
				{
					Debug.LogException(_e);
					_data = default(T);
					return false;
				}

				_data = Dec<T>(s);
				return true;
			}
		}
	}

}