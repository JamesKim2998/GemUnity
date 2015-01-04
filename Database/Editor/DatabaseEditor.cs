using UnityEditor;
using UnityEngine;
using System.Collections;

public class DatabaseEditor<Type, Data> : Editor
	where Data : Component, IDatabaseKey<Type>
{
	private Database<Type, Data> m_This;

	void OnEnable()
	{
		m_This = (Database<Type, Data>) target;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (GUILayout.Button("Build"))
			m_This.Build();
	}
}
