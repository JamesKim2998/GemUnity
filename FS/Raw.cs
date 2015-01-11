using System;
using System.Collections.Generic;
using System.IO;

namespace Gem
{
	public static class Raw
	{
		public static List<Path_> searchPaths = new List<Path_> { new Path_("./Resources") };

		public static FullPath? FullPath(Path_ _path)
		{
			foreach (var _searchPath in searchPaths.GetReverseEnum())
			{
				var _fullPath = _searchPath / _path;
				if (File.Exists(_fullPath))
					return new FullPath(_fullPath);
			}

			L.W(L.M.RSC_NOT_EXISTS(_path));
			return null;
		}

		public static StreamReader Read(FullPath _path, FileMode _mode, FileAccess _access = FileAccess.Read)
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