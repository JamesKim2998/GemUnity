using System;
using UnityEngine;

public class Emitter : MonoBehaviour 
{
	public EmitterType type;

	public int owner { get; set; }
	[HideInInspector]
	public string ownerPlayer;
    public Rigidbody2D ownerBody { get; set; }
	
    #region editor properties
    public bool useDamage = true;
	public int editorDamage = 1;
    public bool useShootBundle = false;
	public int editorShootBundle = 1;
    #endregion

	#region state
	public enum State {
		IDLE,
		PREPARING,
		CHARGING,
		SHOOTING,
		COOLING,
	}

	private State m_State = State.IDLE;
	public State state { 
		get { return m_State; } 
		private set {
			if (state == value) 
			{
				Debug.Log("Trying to set same state again. Ignore.");
				return;
			}
	
			stateTime = 0;
			m_State = value; 

			switch (state) 
			{
			case State.SHOOTING:
				++shootCount;
				++shootIdx;
				ShootProc();
				break;
			case State.COOLING:
				shootIdx = 0;
				if (postCooldown != null) postCooldown(this); 
				break;
			}
		}
    }

    public bool IsState(State _state) { return _state == m_State; }

    // time
    public float stateTime { get; private set; }

    public float prepareTime;
    public float chargeTime;
    public float shootTime;
    public float cooldown;

	#endregion

	#region shoot option

	public bool autoload = false;

	public int shootBundle
    {
        get { return doGetBundle != null ? doGetBundle(this) : 1; }

	    set
        {
            if (doGetBundle != null)
                Debug.LogWarning("doGetBundle already exists. Replace.");
            doGetBundle = delegate { return value; };
        }
    }

	public int shootAtOnce = 1;
	#endregion

    #region ammo
	public int ammo { get; set; }
	public int ammoMax = 0;
	public bool ammoInfinite = true;
	public bool HasAmmo() { return ammoInfinite || ammo >= 0; }
	public Action<Emitter> postOutOfAmmo;
    #endregion

	#region projectile properties
    public int? damage { get; set; }
    public bool relativeVelocityEnabled = true;
    public Vector2 projectileOffset;
    #endregion

	#region projectile construction
	// shoot/projectile idx
	public int shootCount { get; private set; }
	public int shootIdx { get; private set; }

	public int projectileCount { get; private set; }
	public int projectileIdx { get; private set; }

    // projectile construct
	public delegate bool DoIsShootable(Emitter _self);
	public DoIsShootable doIsShootable;
	
	public delegate int DoGetBundle(Emitter _self);
	public DoGetBundle doGetBundle;

	public delegate GameObject DoCreateProjectile(Emitter _self);
	public DoCreateProjectile doCreateProjectile;

	public Action<Emitter, GameObject> doShoot;

	public delegate GameObject DoCreateProjectileServer(int _count, int _idx);
	public DoCreateProjectileServer doCreateProjectileServer;
	#endregion

	#region event
	public Action<Emitter, Projectile> postShoot;
	public Action<Emitter> postCooldown;
	#endregion

    #region network
    public bool IsNetworkEnabled()
    {
        return networkView && networkView.isMine && networkView.enabled && (Network.peerType != NetworkPeerType.Disconnected);
    }
    #endregion

    #region misc properties
	public DeadzoneField deadzone;
    #endregion

    public Emitter()
    {
        ammo = 0;
        shootIdx = 0;
        shootCount = 0;
        projectileCount = 0;
    }

    public void Awake()
    {
		ammo = ammoInfinite ? int.MaxValue : ammoMax;
        if (useDamage) damage = editorDamage;
        if (useShootBundle) shootBundle = editorShootBundle;
    }

	public void Update () 
	{
		if (networkView && networkView.enabled && ! networkView.isMine)
			return;

		stateTime += Time.deltaTime;

		switch (m_State) 
		{
		case State.IDLE: break;
		case State.SHOOTING: {
			if (stateTime >= shootTime)
			{
				if (HasAmmo() && (autoload || (shootIdx < shootAtOnce)))
					state = State.CHARGING;
				else 
					state = State.COOLING;
			}
			break;
		}

		case State.CHARGING: {
			if (stateTime >= chargeTime)
				state = State.SHOOTING;
			break;
		}

		case State.COOLING: {
			if (stateTime >= cooldown)
				state = State.IDLE;
			break;
		}
		}
	}

	public bool IsShootable() {
		return HasAmmo()
            && IsState(State.IDLE)
			&& stateTime >= prepareTime
            && (doIsShootable == null || ! doIsShootable(this));
	}

	public bool TryShoot()
	{
		if (IsShootable())
		{
			Shoot();
			return true;
		}

		return false;
	}

	public void Shoot() {
		if (Debug.isDebugBuild)
		{
			if (! IsShootable()) 
				Debug.LogError("Trying to shoot but is not shootable. Continue anyway.");
		}

		state = State.SHOOTING;
	}

	private void ShootProc() 
	{
	    var _bundle = shootBundle;

		for (projectileIdx = 0; projectileIdx < _bundle && HasAmmo(); ++projectileIdx) 
        {
			var _projectileGO = doCreateProjectile(this);

			++projectileCount;

			if (! ammoInfinite) --ammo;

			_projectileGO.transform.rotation *= transform.rotation;
			_projectileGO.transform.position += transform.position;
			_projectileGO.transform.Translate(projectileOffset);

			var _projectile = _projectileGO.GetComponent<Projectile>();
			_projectile.owner = owner;
			_projectile.ownerPlayer = ownerPlayer;
			_projectile.ownerEmitter = type;
			if (damage.HasValue) _projectile.damage = damage.Value;

			if (ownerBody && relativeVelocityEnabled) 
				_projectileGO.rigidbody2D.velocity += ownerBody.velocity;

			var _projectileDeadzone = _projectile.GetComponent<ProjectileDecoratorDeadzone>();
			if (_projectileDeadzone && deadzone) _projectileDeadzone.deadzone = deadzone.deadzone;

			if (doShoot != null) 
				doShoot(this, _projectileGO);

            if (IsNetworkEnabled())
			{
				_projectileGO.networkView.viewID = Network.AllocateViewID();
				_projectileGO.networkView.enabled = true;

				networkView.RPC("Emitter_RequestCreateProjectileServer", 
				                RPCMode.Others, 
				                _projectile.networkView.viewID, 
				                Network.player.guid,
				                _projectileGO.transform.position, 
				                _projectileGO.transform.localRotation, 
				                (Vector3) _projectileGO.rigidbody2D.velocity, 
				                projectileCount, 
				                projectileIdx);
			}

			if (postShoot != null) 
				postShoot(this, _projectileGO.GetComponent<Projectile>());
		}

		if (!HasAmmo())
		{
			if (postOutOfAmmo != null)
				postOutOfAmmo(this);
			Rest();
		}
	}

	[RPC]
	void Emitter_RequestCreateProjectileServer(
		NetworkViewID _viewID, 
		string _ownerPlayer, 
		Vector3 _position, 
		Quaternion _rotation, 
		Vector3 _velocity, int _count, int _idx)
	{
		var _projectileGO = doCreateProjectileServer((int) _count, (int) _idx);

		_projectileGO.transform.position = _position;
		_projectileGO.transform.rotation = _rotation;
		_projectileGO.rigidbody2D.velocity = _velocity;

        var _projectile = _projectileGO.GetComponent<Projectile>();
		_projectile.owner = owner;
		_projectile.ownerPlayer = _ownerPlayer;
        _projectile.ownerEmitter = type;

        _projectileGO.networkView.viewID = _viewID;
        _projectileGO.networkView.enabled = true;
	}

	public void Rest() 
	{
		if (! autoload)
			return;

		switch (state)
		{
		case State.SHOOTING:
		case State.CHARGING:
			state = State.COOLING;
			break;
		case State.PREPARING:
			state = State.IDLE;
			break;
		}
	}
}
