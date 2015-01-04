using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Projectile))]
public class ProjectileDecoratorDeadzone : MonoBehaviour 
{
    private int m_DeadzoneColliderID = 0;

    public Collider2D deadzone
    {
        set { m_DeadzoneColliderID = value != null ? value.GetInstanceID() : 0; }
    }

	void Start ()
	{
	    if (m_DeadzoneColliderID == 0)
	    {
            Debug.LogWarning("Deadzone does not have colliderID. Destroy self.");
            Destroy(this);
	    }

	    GetComponent<Projectile>().Deactivate();
	}

    void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.GetInstanceID() == m_DeadzoneColliderID) 
        {
            GetComponent<Projectile>().Activate();
            Destroy(this);
        }
    }

}
