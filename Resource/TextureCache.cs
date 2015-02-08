using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Gem
{
	using Cache = Dictionary<int, WeakReference>;

	public static class TextureCache
	{
		private static readonly Cache sCache = new Cache();

		public static Texture2D Load(FullPath _path)
		{
			var _key = _path.GetHashCode();

			WeakReference _cached;
			if (sCache.TryGetValue(_key, out _cached))
			{
				if (_cached.IsAlive)
				{
					var _tex = (Texture2D)_cached.Target;	
					if (_tex) return _tex;
				}
			}
			
			var _texNew = DoLoad(_path);
			sCache[_path.GetHashCode()] = new WeakReference(_texNew);
			return _texNew;
		}

		private static Texture2D DoLoad(FullPath _path)
		{
			Texture2D _tex = null;
			if (File.Exists(_path))
			{
				var _fileData = File.ReadAllBytes(_path);
				_tex = new Texture2D(2, 2);
				_tex.LoadImage(_fileData); // auto-resize
			}
			return _tex;
		}

	}
}