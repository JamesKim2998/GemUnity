using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Gem
{
	public static class SerializeHelper
	{
		public static MemoryStream Serialize(this object o)
		{
			var s = new MemoryStream();
			var _formatter = new BinaryFormatter();
			_formatter.Serialize(s, o);
			return s;
		}

		public static void SerializeToFile(this object o, string _path)
		{
			using (var s = o.Serialize())
			using (var f = new FileStream(_path, FileMode.Create, FileAccess.Write))
				s.WriteTo(f);
		}

		public static T Deserialize<T>(this MemoryStream s)
		{
			IFormatter _formatter = new BinaryFormatter();
			s.Seek(0, SeekOrigin.Begin);
			return (T) _formatter.Deserialize(s);
		}

		public static T DeserializeFile<T>(string _path)
		{
			using (var s = new MemoryStream())
			using (var f = new FileStream(_path, FileMode.Open, FileAccess.Read))
			{
				var _bytes = new byte[f.Length];
				f.Read(_bytes, 0, (int)f.Length);
				s.Write(_bytes, 0, (int)f.Length);
				return Deserialize<T>(s);
			}
		}
	}

}