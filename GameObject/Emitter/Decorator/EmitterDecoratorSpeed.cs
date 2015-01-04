
using UnityEngine;

[RequireComponent(typeof(Emitter))]
public class EmitterDecoratorSpeed : MonoBehaviour
{
    private Emitter m_Emitter;
	public float speed;

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
        _projectile.rigidbody2D.velocity += (Vector2) (speed * _emitter.transform.right);
    }

}
