using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public interface IDatabase
{
	void Build();
}

public class Database<Type, Data> : MonoBehaviour, IDatabase, IEnumerable<Data>
	where Data : Component, IDatabaseKey<Type>
{
    public List<Data> dataPrfs;

    private readonly Dictionary<Type, Data> m_DataDict = new Dictionary<Type, Data>();
	private readonly List<Data> m_DataList = new List<Data>();

    public Data this[Type _type]
    {
        get
        {
            Data data;
            if (m_DataDict.TryGetValue(_type, out data))
            {
                return data;
            }
            else
            {
                Debug.LogWarning("Trying to access " + _type + ", but data does not exist. Return null.");
                return default(Data);
            }
        }
    }

    public IEnumerator<Data> GetEnumerator()
    {
		return m_DataList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
		return m_DataList.GetEnumerator();
    }

    void Start()
    {
	    Build();
    }

	public virtual void Build()
	{
		m_DataDict.Clear();
		m_DataList.Clear();

		if (Debug.isDebugBuild)
		{
			dataPrfs.RemoveAll(_dataPrf => _dataPrf == null);
			dataPrfs = dataPrfs.Distinct().ToList();
		}

		foreach (var _dataPrf in dataPrfs)
		{
			m_DataDict.Add(_dataPrf.Key(), _dataPrf);
			m_DataList.Add(_dataPrf);
		}
	}
}
