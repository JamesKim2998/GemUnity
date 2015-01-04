using System;
using UnityEngine;
using System.Collections;

public class DamageDetector : MonoBehaviour 
{
	public int owner;
	public float delay;
	public float delayLeft { get; private set; }

	public Action<AttackData> postDamage { get; set; }

	void OnEnable()
	{
		delayLeft = 0;
	}

	void Update()
	{
		delayLeft -= Time.deltaTime;
	}

	public bool IsDamagable()
	{
		return enabled && delayLeft <= 0;
	}

	public void Damage(AttackData attackData) {
		if (! IsDamagable()) 
			return;

		delayLeft = delay;

		if (postDamage != null) 
			postDamage(attackData);
		else
			Debug.Log("postDamage is not set!");
	}

}
