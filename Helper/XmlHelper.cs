using System.Xml;

namespace Gem
{
	public static class XmlHelper
	{
		public enum ValueType
		{
			BOOL,
			INT, 
			DOUBLE,
			STRING,
		}

		public static int AsInt(this XmlAttribute _attr)
		{
			return int.Parse(_attr.Value);
		}

		public static ValueType ParseValue(string _val, out object _ret)
		{
			int _intVal;
			if (int.TryParse(_val, out _intVal))
			{
				_ret = _intVal;
				return ValueType.INT;
			}

			double _dbVal;
			if (double.TryParse(_val, out _dbVal))
			{
				_ret = _dbVal;
				return ValueType.DOUBLE;
			}

			bool _blVal;
			if (bool.TryParse(_val, out _blVal))
			{
				_ret = _blVal;
				return ValueType.BOOL;
			}

			_ret = _val;
			return ValueType.STRING;
		}

		public static bool CheckAndAssign(this XmlAttributeCollection _attrs, string _name, ref bool _val)
		{
			var _attr = _attrs[_name];
			if (_attr == null) return false;
			_val = bool.Parse(_attr.Value);
			return true;
		}

		public static bool? CheckAttributesAndAssign(this XmlDocument _xml, string _name, ref bool _val)
		{
			if (_xml.Attributes == null) return false;
			return _xml.Attributes.CheckAndAssign(_name, ref _val);
		}
	}
}