using System.IO;
using Gem;
using Gem.In;
using Newtonsoft.Json;
using UnityEngine;

public class InputTest : MonoBehaviour
{

	public Gem.Gem gem;

	void Start ()
	{
		var _reader = JsonHelper.Resource("input.json");
		foreach (var _fetcher in InputFetcherFactory.Read(_reader))
		{
			D.Log(0, _fetcher.ToString());
		}
	}
	
	void Update () {
	
	}
}
