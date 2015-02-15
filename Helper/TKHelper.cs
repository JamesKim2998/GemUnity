
using UnityEngine;

namespace Gem
{
	public static class TKHelper 
	{
#if UNITY_EDITOR
		public static tk2dSpriteCollection AssetCollection(FullPath _path)
		{
			var _anim = Asset.Load<GameObject>(_path);
			return _anim ? _anim.GetComponent<tk2dSpriteCollection>() : null;
		}

		public static tk2dSpriteAnimation AssetAnimation(FullPath _path)
		{
			var _anim = Asset.Load<GameObject>(_path);
			return _anim ? _anim.GetComponent<tk2dSpriteAnimation>() : null;
		}
#endif
	}


}