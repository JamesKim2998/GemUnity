
using UnityEngine;

namespace Gem
{
	public static class TKHelper 
	{
#if UNITY_EDITOR
		public static tk2dSpriteCollection AssetCollection(Path _path)
		{
			var _anim = Asset.Load<GameObject>(_path);
			return _anim ? _anim.GetComponent<tk2dSpriteCollection>() : null;
		}

		public static tk2dSpriteAnimation AssetAnimation(Path _path)
		{
			var _anim = Asset.Load<GameObject>(_path);
			return _anim ? _anim.GetComponent<tk2dSpriteAnimation>() : null;
		}
#endif
	}


}