using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ArcTargeter))]
public class ArcGuiderEditor : Editor {

    ArcTargeter m_This;
	
	public void OnEnable()
	{
        m_This = (ArcTargeter) target;
	}
	
	public void OnSceneGUI()
	{
		if (! m_This.enabled) return;
		
	    var _radius = m_This.radius;
        var _color = Color.gray;
        _color.a = 0.1f;
		
		Handles.color = _color;

		Handles.DrawSolidArc(
			m_This.transform.position, 
			Vector3.forward, 
			m_This.transform.right,
			m_This.range, 
			_radius);
		
		Handles.DrawSolidArc(
			m_This.transform.position, 
			Vector3.forward, 
			m_This.transform.right,
			-m_This.range, 
			_radius);
	}
	
}
