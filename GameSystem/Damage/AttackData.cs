using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

[System.Serializable]
public class AttackData 
{
	public static readonly AttackData DAMAGE_MAX = new AttackData(int.MaxValue);

	public string ownerPlayer;

	public EmitterType emitter = EmitterType.NONE;
    public ProjectileType projectile = ProjectileType.NONE;

	[System.NonSerialized]
	public Vector2 velocity = Vector2.zero;
	public int damage = 0;

	public AttackData(int _damage)
	{
		damage = _damage;
	}

	public string Serialize() 
	{
		return Serializer.Serialize(this)
	        + Serializer.Serialize(velocity);
	}

	public static AttackData Deserialize(string _serial)
	{
	    Vector2 _velocity;
        _serial = _serial.Substring(0, Serializer.Deserialize(_serial, out _velocity));

        AttackData _attackData;
		Serializer.Deserialize(_serial, out _attackData);

		_attackData.velocity = _velocity;
		return _attackData;
	}

	public static implicit operator int(AttackData _attackData)
	{
		return _attackData.damage;
	}
}
