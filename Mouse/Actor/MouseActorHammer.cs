using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MouseActor))]
public class MouseActorHammer : MonoBehaviour {

	void Start ()
	{
		GetComponent<MouseActor>().act = _go =>
		{
			Destroy(_go);
			return true;
		};
	}
	
}
