using UnityEngine;
using System.Collections;

public class ProjectileFX : MonoBehaviour {

	// effect
	public GameObject effectHitPrf;
	public Vector3 effectHitOffset;
	
	void Start ()
	{
		var _projectile = GetComponent<Projectile>();
		_projectile.postHit += ListenHit;
	}

	void OnDestroy()
	{
		var _projectile = GetComponent<Projectile>();
		_projectile.postHit -= ListenHit;
	}

	void ListenHit (Projectile _projectile, Collider2D _collider) 
	{
		if (effectHitPrf)
		{
			var _effectHit = (GameObject)Instantiate(effectHitPrf, transform.position, transform.rotation);
			_effectHit.transform.Translate(effectHitOffset);
		}
	}
}
