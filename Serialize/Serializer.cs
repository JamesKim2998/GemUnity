using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

static class Serializer
{
	public static string Serialize<T>(T _data)
	{
		var _bf = new BinaryFormatter();
		var _os = new MemoryStream();
		_bf.Serialize(_os, _data);
		return Convert.ToBase64String(_os.GetBuffer());
	}

	public static void Deserialize<T>(string _data, out T _out)
	{
		var _bf = new BinaryFormatter();
		var _is = new MemoryStream(Convert.FromBase64String(_data));
		_out = (T)_bf.Deserialize(_is);
	}

    public static string Serialize(Vector2 _value)
    {
        return "(" + _value.x + ',' + _value.y + ')';
    }

    public static int Deserialize(string _serial, out Vector2 _value)
    {
        var _serialEnd = _serial.LastIndexOf(')');

        var _commaIdx = _serial.LastIndexOf(',', _serialEnd - 1);
        _value.y = float.Parse(_serial.Substring(_commaIdx + 1, _serialEnd - _commaIdx - 1));

        var _serialStart = _serial.LastIndexOf('(', _commaIdx - 1);
        _value.x = float.Parse(_serial.Substring(_serialStart + 1, _commaIdx - _serialStart - 1));

        return _serialStart;
    }
}
