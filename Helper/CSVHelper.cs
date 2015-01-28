using System;
using System.Collections.Generic;
using System.Linq;
using Nvv.IO.CSV.Cs;

namespace Gem
{
	using Fields = CSVReader.CSVFields;

	public struct CSVValue
	{
		private readonly CSVField mField;
		public CSVField val { get { return mField; } }

		public CSVValue(CSVField _field)
		{
			mField = _field;
		}

		public static implicit operator string(CSVValue _this)
		{
			return _this.val.Value;
		}

		public void Parse(out List<string> _val)
		{
			_val = new List<string>(CSVHelper.Split(val.Value));
		}
	}

	public struct CSVRow
	{
		private int mIdx;
		private readonly Fields mFields;

		public CSVRow(Fields _fields)
		{
			mIdx = 0;
			mFields = _fields;
		}

		public CSVValue Read()
		{
#if UNITY_EDITOR
			if (mIdx >= mFields.Count)
			{
				L.E(L.M.ARR_EXCEED("fields", mIdx));
				return new CSVValue(new CSVField());
			}
#endif

			return new CSVValue(mFields[mIdx++]);
		}
	}

	public static class CSVHelper
	{
		public static CSVReader Open(FullPath _path)
		{
			var _reader = new CSVFileReader
			{
				FileName = _path, 
				ASCIIonly = false,
#if UNITY_EDITOR
				HeaderPresent = true
#endif
			};

#if UNITY_EDITOR
			try
			{
#endif
				_reader.Open();
#if UNITY_EDITOR
			}
			catch (Exception)
			{
				L.E(L.M.RSC_NOT_EXISTS(_path));
				var _ret = new CSVStringReader();
				_ret.Open();
				_ret.Next();
				return _ret;
			}
#endif

#if !UNITY_EDITOR
			_reader.Next();
#endif

			return _reader;
		}

		public static CSVRow Row(this CSVReader _csv)
		{
			return new CSVRow(_csv.Fields);
		}

		public static IEnumerable<string> Split(string _val)
		{
			if (string.IsNullOrEmpty(_val))
			{
				L.D("trying to split empty string.");
				return Enumerable.Empty<string>();
			}
			else
			{
				// todo: 최적화
				return _val.Split('|');
			}
		}
	}
}