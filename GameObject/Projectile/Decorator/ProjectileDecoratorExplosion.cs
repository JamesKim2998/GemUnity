using UnityEngine;

public class ProjectileDecoratorExplosion : MonoBehaviour {
	
	private Projectile m_Projectile;
	public Explosion explosionPrf;
	private bool m_Exploded = false;
	
	void Start () 
	{
		m_Projectile = GetComponent<Projectile>();
		m_Projectile.postHit += Explode;
		m_Projectile.postBumped += Explode;
	}

    void OnDestroy()
    {
        m_Projectile.postHit -= Explode;
        m_Projectile.postBumped -= Explode;
    }

	void Explode(Projectile _projectile, Collider2D _collider) 
	{
		if (m_Exploded) 
		{
			Debug.Log("Trying to explode multiple times. Ignore.");
			return;
		}

		ExplodeOnCrash_RequestExplode(transform.position);

		if (Network.peerType == NetworkPeerType.Server) 
            networkView.RPC("ExplodeOnCrash_RequestExplode", RPCMode.Others, transform.position);
	}

	[RPC]
	void ExplodeOnCrash_RequestExplode(Vector3 _position) 
	{
		if (m_Exploded) return;

		m_Exploded = true;

        var _explosionGO = (GameObject)Instantiate(explosionPrf.gameObject, _position, Quaternion.identity);

        var _field = _explosionGO.GetComponent<DamageField>();
        _field.attackData = m_Projectile.attackData;
	    _field.targetMask = m_Projectile.collisionTargets;

        var _explosion = _explosionGO.GetComponent<Explosion>();
        _explosion.Explode();
	}

}
