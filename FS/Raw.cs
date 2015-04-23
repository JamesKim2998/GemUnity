using System;
using System.Collections.Generic;
using System.IO;

namespace Gem
{
	public static class Raw
	{
		public static List<Directory> searchPaths = new List<Directory> { new Directory("./Resources") };

		public static Path? FullPath(Path _path)
		{
			foreach (var _searchPath in searchPaths.GetReverseEnum())
			{
				var _fullPath = _searchPath / _path;
				if (File.Exists(_fullPath))
					return new Path(_fullPath);
			}

			L.W(L.M.RSC_NOT_EXISTS(_path));
			return null;
		}

		public static StreamReader Read(Path _path, FileMode _mode, FileAccess _access = FileAccess.Read)
		{
			try
			{
				return new StreamReader(new FileStream(_path, _mode, _access));
			}
			catch (Exception)
			{
				L.E(L.M.RSC_NOT_EXISTS(_path));
				return null;
			}
		}
	}

}