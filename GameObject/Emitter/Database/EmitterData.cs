using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class EmitterData : MonoBehaviour, IDatabaseKey<EmitterType>
{
	public EmitterType type;
	public EmitterType Key() { return type; }

	public Emitter emitterPrf;


	public Emitter Instantiate()
	{
		return ((GameObject) Instantiate(emitterPrf.gameObject)).GetComponent<Emitter>();
	}
}

