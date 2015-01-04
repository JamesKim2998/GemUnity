using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Explosion : MonoBehaviour
{
    public CircleCollider2D field;

    public bool executeOnStart = true;

	public float radius;
	public float duration;

    public float impulse;
    public LayerMask impulseMask;
    public float impulseRadius;

    public GameObject scaleTarget;

    private bool m_Exploded = false;
	private float m_ExplosionTime = 0;

	void Start()
	{
        if (field) 
            field = collider2D as CircleCollider2D;

        if (executeOnStart) 
            Explode();
	}

	void Update()
	{
	    if (! m_Exploded) return;
		m_ExplosionTime += Time.deltaTime;

        var _radius = radius * m_ExplosionTime / duration;
        field.radius = _radius;
        if (scaleTarget) scaleTarget.transform.localScale = new Vector3(_radius, _radius, 1);
	}

    void Impulse()
    {
        var _rayResults = Physics2D.OverlapCircleAll(transform.position, impulseRadius, impulseMask);
        foreach (var _rayResult in _rayResults)
        {
            if (! _rayResult.rigidbody2D) continue;
            var _delta = _rayResult.transform.position - transform.position;
            var _distance = _delta.magnitude;
            var _direction = _delta/_distance;
            var _factor = (_distance < radius) ? 1.0f
                : (impulseRadius - _distance) / (impulseRadius - radius);
            var _impulse = impulse * _factor * _direction;
            _rayResult.rigidbody2D.AddForce(_impulse, ForceMode2D.Impulse);
        }
    }

	public void Explode() {
	    if (m_Exploded)
	    {
            Debug.LogWarning("Trying to explode again. Ignore.");
	        return;
	    }

		m_Exploded = true;
        Invoke("Impulse", duration / 2);
		Invoke("DestroySelf", duration);
	}

	void DestroySelf() {
		Destroy(gameObject);
	}
}
