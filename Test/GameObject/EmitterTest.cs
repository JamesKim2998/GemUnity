using UnityEngine;
using System.Collections;

public class EmitterTest : MonoBehaviour
{
    public Emitter emitter;

    public void Shoot()
    {
        if (emitter.IsShootable())
            emitter.Shoot();
    }
}
