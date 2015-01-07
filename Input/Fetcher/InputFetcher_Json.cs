using LitJson;
using UnityEngine;

namespace Gem.In
{
	public partial class InputFetcherButton
	{
		private struct Form
		{
			public string name;
		}

		public InputFetcherButton(JsonReader _reader)
		{
			var _form = JsonMapper.ToObject<Form>(_reader);
			name = _form.name;
		}
		
		public override string ToString()
		{
			return JsonMapper.ToJson(new Form{ name = name });
		}
	}

	public partial class InputFetcherKey
	{
		struct Form
		{
			public string code;
		}

		public InputFetcherKey(JsonReader _reader)
		{
			var _form = JsonMapper.ToObject<Form>(_reader);
			EnumHelper.TryParse(_form.code, out code);
		}
		
		public override string ToString()
		{
			return JsonMapper.ToJson(new Form{ code = code.ToString() });
		}
	}

}