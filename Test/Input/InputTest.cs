using Gem;
using Gem.In;
using LitJson;
using UnityEngine;

public class InputTest : MonoBehaviour
{

	public Gem.Gem gem;

	void Start ()
	{
		var _data = JsonHelper.DataWithRsc("input.json");
		var _map = gem.input.map;

		foreach (var _kv in InputFetcherFactory.Read(_data))
			_map.Add(_kv.Key, _kv.Value);
	}
	
	void Update () {
	
	}
}
