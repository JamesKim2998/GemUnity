using System.IO;
using UnityEditor;

public static class EditorSceneHelper {

	public static void Transfer(string _scene)
	{
		if (Path.GetFileName(EditorApplication.currentScene) == Path.GetFileName(_scene))
			return;
		EditorApplication.SaveCurrentSceneIfUserWantsTo();
		EditorApplication.OpenScene(_scene);
	}

}
