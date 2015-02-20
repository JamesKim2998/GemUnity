using UnityEngine;

namespace Gem
{
	public static class TextureHelper
	{
		public static Sprite CreateSpite(this Texture2D _this)
		{
			return CreateSpite(_this, new Vector2(0.5f, 0.5f));
		}

		public static Sprite CreateSpite(this Texture2D _this, Vector2 _pivot)
		{
			if (!_this) return null;
			return Sprite.Create(_this, new Rect(0, 0, _this.width, _this.height), _pivot);
		}
	}

}