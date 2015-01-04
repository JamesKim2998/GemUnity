using UnityEngine;
using System.Collections;

public class EmitterDecoratorRecoil : MonoBehaviour {

	public float recoil = 0;
	public Vector2 recoilModifier = Vector2.one;

	private Emitter m_Emitter;

	void Start ()
	{
		m_Emitter = GetComponent<Emitter>();
		m_Emitter.postShoot += ListenShoot;
	}

	void OnDestroy()
	{
		if (m_Emitter) m_Emitter.postShoot -= ListenShoot;
	}


	void ListenShoot(Emitter _emitter, Projectile _projectile)
	{
		if (! _emitter.ownerBody)
		{
			Debug.LogWarning("Emitter doesn't have owner body. Ignore.");
			return;
		}

		if (Mathf.Approximately(recoil, 0))
			return;

		var _recoil = recoil * -transform.right;
		_recoil.x *= recoilModifier.x;
		_recoil.y *= recoilModifier.y;

		_emitter.ownerBody.AddForceAtPosition(
			_recoil, transform.position, 
			ForceMode2D.Impulse);
	}
}
