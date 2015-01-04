using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(Emitter))]
public class EmitterEditor : Editor
{
	private Emitter m_Target;

	void OnEnable()
	{
		m_Target = (Emitter) target;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (PrefabHelper.IsPrefab(m_Target))
			return;

		if (GUILayout.Button("Shoot"))
			m_Target.TryShoot();
	}
}
