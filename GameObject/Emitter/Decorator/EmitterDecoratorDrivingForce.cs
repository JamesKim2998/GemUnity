
using UnityEngine;

[RequireComponent(typeof(Emitter))]
public class EmitterDecoratorDrivingForce : MonoBehaviour
{
    private Emitter m_Emitter;
	public float drivingForce;
	public float explosionRadius;

	void Awake() 
	{
		m_Emitter = GetComponent<Emitter>();
        m_Emitter.doShoot += DoShoot;
	}

    void OnDestroy()
    {
        m_Emitter.doShoot -= DoShoot;
    }

    void DoShoot(Emitter _emitter, GameObject _projectile)
    {
        _projectile.GetComponent<Projectile>().drivingForce = drivingForce * _emitter.transform.right;
    }
}
