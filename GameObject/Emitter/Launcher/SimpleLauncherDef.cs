using UnityEngine;

[RequireComponent(typeof(Emitter))]
public class SimpleLauncherDef : MonoBehaviour 
{
	public ProjectileType projectile;

	private Projectile m_ProjectilePrf;
	public Projectile projectilePrf
	{
		get
		{
			if (m_ProjectilePrf) return m_ProjectilePrf;
			m_ProjectilePrf = ProjectileDatabase.shared[projectile].projectilePrf;
			return m_ProjectilePrf;
		}
	}

	void Start() 
	{
		var _emitter = GetComponent<Emitter>();

#if DEBUG
		if (_emitter == null) 
		{
			Debug.LogError("Emitter is not exists! Ignore.");
			return;
		}
#endif

		_emitter.doCreateProjectile = delegate { return (GameObject)Instantiate(projectilePrf.gameObject); };

		_emitter.doCreateProjectileServer = delegate { return (GameObject)Instantiate(projectilePrf.gameObject); };
	}
	
}
