using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(Explosion))]
public class ExplosionEditor : Editor
{

    Explosion m_This;

    public void OnEnable()
    {
        m_This = (Explosion) target;
    }

    public void OnSceneGUI()
    {
        if (!m_This.enabled) return;

        Handles.color = new Color(1, 0, 0, 0.1f);
        Handles.DrawSolidArc(
            m_This.transform.position,
            Vector3.forward,
            m_This.transform.right,
            360,
            m_This.radius);

        Handles.color = new Color(0.5f, 0.5f, 0, 0.5f);
        Handles.DrawWireArc(
            m_This.transform.position,
            Vector3.forward,
            m_This.transform.right,
            360,
            m_This.impulseRadius);
    }
	
}
