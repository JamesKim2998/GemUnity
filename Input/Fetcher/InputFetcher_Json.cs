using Newtonsoft.Json;
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
			var _form = JsonHelper.Convert<Form>(_reader);
			name = _form.name;
		}
		
		public override string ToString()
		{
			return JsonHelper.Convert(new Form{ name = name });
		}
	}

	public partial class InputFetcherKey
	{
		struct Form
		{
			public KeyCode code;
		}

		public InputFetcherKey(JsonReader _reader)
		{
			var _form = JsonHelper.Convert<Form>(_reader);
			code = _form.code;
		}
		
		public override string ToString()
		{
			return JsonHelper.Convert(new Form{ code = code });
		}
	}

}