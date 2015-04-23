using System;
using System.Diagnostics;
using System.IO;

namespace Gem
{
	[DebuggerDisplay("Path = {mValue}")]
	[Serializable]
	public struct Path
	{
		private readonly string mValue;

		public Path(string _path)
		{
			mValue = _path;
		}

		public bool Exists()
		{
			return File.Exists(mValue);
		}

		public static implicit operator string(Path _this)
		{
			return _this.mValue;
		}
	}

	[DebuggerDisplay("Directory = {mValue}")]
	[Serializable]
	public struct Directory
	{
		private readonly string mValue;

		public Directory(string _dir)
		{
			mValue = _dir;
		}

		public bool Exists()
		{
			return System.IO.Directory.Exists(mValue);
		}

		public bool CreateIfNotExists()
		{
			var _found = !Exists();
			if (_found)
				System.IO.Directory.CreateDirectory(mValue);
			return _found;
		}

		public static Path operator /(Directory _this, Path _append)
		{
			return new Path(_this.mValue + "/" + _append);
		}
	}
}