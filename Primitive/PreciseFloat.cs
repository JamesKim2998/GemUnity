using UnityEngine;
using System.Collections;

public struct PreciseFloat
{
	public PreciseFloat(int _val) 
    {
		mRaw = _val * PRECISION; 
    }

	public PreciseFloat(float _val)
	{
		mRaw = (int) _val * PRECISION;
	}

	private PreciseFloat(int _val, int _rawTag)
	{
		mRaw = _val;
	}

	public static implicit operator PreciseFloat(int _val) 
    {
		return new PreciseFloat(_val);
    }

	public static implicit operator PreciseFloat(float _val)
	{
		return new PreciseFloat(_val);
	}

	public static implicit operator int(PreciseFloat _pf)
	{
		return _pf.mRaw / PRECISION;
	}

	public static implicit operator float(PreciseFloat _pf)
	{
		return _pf.mRaw / (float) PRECISION;
	}

	public static PreciseFloat operator +(PreciseFloat a, PreciseFloat b)
	{
		return new PreciseFloat(a.mRaw + b.mRaw, 0);
	}

	public static PreciseFloat operator -(PreciseFloat a)
	{
		return new PreciseFloat(-a.mRaw, 0);
	}

	public static PreciseFloat operator -(PreciseFloat a, PreciseFloat b)
	{
		return a + (-b);
	}

	private const int PRECISION = 256;
	private readonly int mRaw;
}