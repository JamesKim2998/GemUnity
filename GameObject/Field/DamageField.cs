using UnityEngine;
using System.Collections;

public class DamageField : MonoBehaviour {

	public AttackData attackData;
	public LayerMask targetMask;
	
	void OnTriggerEnter2D (Collider2D _collider)
	{
		if (! enabled) return;

		if (LayerHelper.Exist(targetMask, _collider)) {
			var detector = _collider.GetComponent<DamageDetector>();
			if (detector) detector.Damage(attackData);
		}
	}
}
