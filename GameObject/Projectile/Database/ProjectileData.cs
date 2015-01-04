using UnityEngine;
using System.Collections;

public class ProjectileData : MonoBehaviour, IDatabaseKey<ProjectileType>
{
	public ProjectileType type;
	public ProjectileType Key() { return type; }

	public Projectile projectilePrf;
}

