using System;
using UnityEngine;
using System.Collections;

public class ArcTargeter : MonoBehaviour 
{
	public GameObject target;
    public bool autoTargeting = true;
    public bool guiderOnly = true;
    public LayerMask targetMask;
    public float retargetDelay = 0.5f;
    private float m_RetargetTimer;

    public int range = 180;
	public float radius;

	public float constCatchUp = 5;
	public float lerpCatchUp = 0.02f;

    bool ShouldUpdate()
    {
        return Network.peerType == NetworkPeerType.Disconnected
            || Network.peerType == NetworkPeerType.Server;
    }

    bool CanTarget()
    {
        return m_RetargetTimer <= 0;
    }

    bool TryTarget(GameObject _obj)
    {
        var _targetPosition = _obj.transform.position - transform.position;
        var _sqrDistance = _obj.transform.position.sqrMagnitude;
        if (_sqrDistance > radius*radius)
            return false;

        var _rotation = Quaternion.FromToRotation(rigidbody2D.velocity, _targetPosition);
        var _angle = _rotation.eulerAngles.z;
        if (_angle > 180) _angle -= 360;
        if (Mathf.Abs(_angle) > range) return false;

        target = _obj;
        var _newAngle = Mathf.Clamp(_angle, -constCatchUp, constCatchUp);
        _newAngle = Mathf.Lerp(_newAngle, _angle, lerpCatchUp);
        var _newVelocity = (Vector3) rigidbody2D.velocity;
        _newVelocity = Quaternion.AngleAxis(_newAngle, Vector3.forward) * _newVelocity;
        rigidbody2D.velocity = _newVelocity;

        return true;
    }

    void Update()
    {
        if (! ShouldUpdate())
            return;

        m_RetargetTimer -= Time.deltaTime;

        if (! CanTarget()) 
            return;

        if (autoTargeting && ! target)
        {
            var _rayResults = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);
            foreach (var _rayResult in _rayResults)
            {
                if (guiderOnly && ! _rayResult.GetComponent<ArcTargeterGuider>())
                    return;

                if (TryTarget(_rayResult.gameObject))
                    return;
            }
        }
    }

	void FixedUpdate ()
    {
        if (!ShouldUpdate())
            return;

		if (target == null)
			return;

	    if (! TryTarget(target))
	    {
	        target = null;
            m_RetargetTimer = retargetDelay;   
	    }
    }
}
