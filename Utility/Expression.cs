using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Gem.Expression
{
	public delegate object Operator(List<object> _args);
	public delegate object Expression();

	public class Bind : Dictionary<string, Operator>
	{
		public static readonly Bind g = new Bind();

		static Bind()
		{
			ExpressionHelper.AddCommon(g);
		}
	}

	public class Solver
	{
		public static readonly Solver g = new Solver();

		private readonly List<Bind> mDics = new List<Bind>(1);

		public Solver()
		{
			Add(Bind.g);
		}

		public void Add(Bind _dic)
		{
			mDics.Add(_dic);
		}

		public bool Remove(Bind _dic)
		{
			return mDics.Remove(_dic);
		}

		private Operator FindOperator(string _key)
		{
			foreach (var _dic in mDics.GetReverseEnum())
			{
				Operator _op;
				if (_dic.TryGetValue(_key, out _op))
					return _op;
			}

			return null;
		}

		private static Expression Identity(object _obj)
		{
			return () => _obj;
		}

		public Expression Build(JToken _expr)
		{
			switch (_expr.Type)
			{
				case JTokenType.Null:
					return null;
				case JTokenType.Boolean:
					return Identity((bool) _expr);
				case JTokenType.Integer:
					return Identity((int) _expr);
				case JTokenType.Float:
					return Identity((float) _expr);
				case JTokenType.String:
					return Identity((string) _expr);
				case JTokenType.Object:
					return Build((JObject) _expr);
				case JTokenType.Array:
				{
					var _func = Build((JArray)_expr);
					return () => _func();
				}
				default:
					L.E(L.M.ENUM_UNDEFINED(_expr.Type));
					return null;
			}
		}

		public Expression Build(JObject _expr)
		{
			var _opKey = (string)_expr["op"];
			var _op = FindOperator(_opKey);
			if (_op == null)
			{
				L.E("op " + _expr + " not found.");
				return null;
			}

			JToken _argsRaw;
			if (!_expr.TryGet("args", out _argsRaw))
				return () => _op(null);

			var _args = Build((JArray)_argsRaw);
			return () => _op(_args());
		}

		public Func<List<object>> Build(JArray _expr)
		{
			var _build = _expr.Select<JToken, Expression>(Build);
			return () =>
			{
				var _ret = new List<object>(_build.Count());
				_ret.AddRange(_build.Select(_var => _var()));
				return _ret;
			};
		}
	}
}