using UnityEngine;
using System.Collections;

public enum EnemyType
{
	Bandit,
	Bear,
	MountainLion,
	Eagle,
	Wolf,
	Skeleton,
	Orc,
	Boss
};

public class Enemy : MonoBehaviour
{
	protected OnState move;//delegate for moving state
	protected OnState attack;//delegate for attack state
	protected OnState lowHealth;//delegate for lowHealth state
	protected OnState die;//delegate for die state
	protected FiniteStateMachine fsm;//fsm to keep track of the enemy's state
	protected float xDirection = 1.0f;
	protected float yDirection = 0.0f;
	protected float attackDistance = 4.5f;
	protected float sizeChangeSpeed = 1.0f;
	public float enemySpeed = 5.0f;
	protected EnemyType enemyType;
	public float enemyHealth = 50.0f;
	protected float lowEnemyHealth;
	public float enemyAttackDamage = 10.0f;
	public float attackTime = 5.0f;
	private float attackTimer = 0.0f;
	private float magicTimer = 0.0f; //timer used to manage magic effects
	private float damageOverTimeAmount;
	protected float enemyYPosition;

	protected Vector2 movement;
	
	public int expGiven = 50;
	//protected bool isDead;
	protected GameObject enemySpawner;
	protected bool isSlowed;
	protected bool isDamageOverTime;
	protected Vector3 actionAreaCenter;
	protected Vector3 size;
	private Color normalColor;

	public float EnemyHealth
	{
		get
		{
			return enemyHealth;
		}
		set
		{
			enemyHealth = value;
		}
	}
	// Use this for initialization
	public virtual void Start () 
	{
		//Set the methods for the delegates
		move = OnMove;
		attack = OnAttack;
		lowHealth = OnLowHealth;
		die = OnDie;
		fsm = new FiniteStateMachine ();
		lowEnemyHealth = enemyHealth * 0.25f;
		rigidbody2D.isKinematic = true;
		actionAreaCenter = GameObject.Find ("ActionArea").transform.renderer.bounds.center;
		enemySpawner = GameObject.Find ("EnemySpawner");
		size = transform.localScale;
		enemyType = EnemyType.Bandit;
		enemyYPosition = transform.position.y;
		//isDead = false;
		normalColor = renderer.material.color;
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
		attackTimer += Time.deltaTime;
		//Move left to right
		if(fsm.GetCurrentState() != attack)
		{
			fsm.PushState(move);//Player should always at least be moving
			//OnMove ();
		}

		//if(enemyHealth <= lowEnemyHealth)
		//{
		//	fsm.PushState(lowHealth);
			//OnLowHealth();
		//}

		if(attackTimer > attackTime && enemyHealth>0 && fsm.GetCurrentState() != attack)
		{
			fsm.PushState(attack);//Switch to attack state
			yDirection = -1.0f;
		}
		
		transform.Translate (movement);

		if(isDamageOverTime)
		{
			Color collideColor = new Color (255, 0, 0, 255);
			StartCoroutine(Flash(collideColor));
			magicTimer -= Time.deltaTime;
			enemyHealth -= damageOverTimeAmount*Time.deltaTime;
			if(magicTimer<=0)
			{
				isDamageOverTime = false;
			}
		}

		if(enemyHealth<=0)
		{
			if(fsm.GetCurrentState() != die)
			{
				fsm.PushState(die);//Switch to die state
				//OnDie();
			}
		}

		Debug.Log("State: " +fsm.GetCurrentState ().Method.Name);//used for testing
		Debug.Log ("Enemy Health: " + enemyHealth);
		fsm.DoState ();//Run the method associate with the current state the enemy is in
	}

	//Flash enemy's color, used to show when enemy is hit
	protected IEnumerator Flash(Color collideColor)
	{
		renderer.material.color = collideColor;
		yield return new WaitForSeconds(0.1f);
		renderer.material.color = normalColor;
	}

	protected virtual void OnMove()
	{
		movement.y = 0;
		movement.x = xDirection * enemySpeed * Time.deltaTime;

		if (Camera.main.WorldToViewportPoint (this.transform.position).x < 0.15) 
		{
			xDirection = 1;
		}
		else if(Camera.main.WorldToViewportPoint (this.transform.position).x > 0.85)
		{
			xDirection = -1;
		}
	}

	protected virtual void OnAttack()
	{
		movement.x = 0;
		movement.y = yDirection * enemySpeed * Time.deltaTime;
		transform.localScale += size*sizeChangeSpeed*Time.deltaTime;
		if(transform.position.y <= actionAreaCenter.y && sizeChangeSpeed >0)
		{
			yDirection = 1.0f;
			sizeChangeSpeed = -1.0f;
			Player.Instance.TakeDamage(this);
		}
		
		if(yDirection>= 0.0f && transform.position.y >= enemyYPosition && sizeChangeSpeed <0)
		{
			//isAttacking = false;
			sizeChangeSpeed = 1.0f;
			attackTimer = 0;
			fsm.PopState();//when attack is finished pop the state off the stack
		}
	}

	protected virtual void OnLowHealth()
	{
		attackTime = 3.0f;
		fsm.PopState ();
	}

	protected virtual void OnDie()
	{
		Player.Instance.AddExperience(expGiven);
		//isDead = true;
		enemySpawner.GetComponent<EnemySpawner> ().NotifyEnemyDied ();
		fsm.PopState ();
		Destroy (gameObject);
	}

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		OnHit (other);
	}

	protected virtual void OnHit(Collider2D col)
	{
		if(col.gameObject.tag == "Magic")
		{
			MagicIcon magicIcon = col.gameObject.GetComponent<MagicIcon>();
			if(magicIcon.State == IconState.Thrown)
			{
				OnHitByMagic(magicIcon);
			}
		}
		else if(col.gameObject.tag == "Ranged")
		{
			RangedIcon rangedIcon = col.gameObject.GetComponent<RangedIcon>();
			if(rangedIcon.State == IconState.Thrown)
			{
				OnHitByRanged(rangedIcon);
			}
		}
		else if(col.gameObject.tag == "Melee")
		{
			WeaponControl control = GameObject.Find("WeaponControl").GetComponent<WeaponControl>();
			if(control.CntrlState == ControlState.Active)
			{
				OnHitByMelee(control);
			}
		}
	}

	public virtual void TakeDamage(float damageAmount)
	{
		enemyHealth -= damageAmount;
	}

	public virtual void SlowDown(float speedDecrease, float timeOver)
	{
		float resetEnemySpeed = enemySpeed;
		enemySpeed -= speedDecrease;
		StartCoroutine (BackToFullSpeed(resetEnemySpeed,timeOver));
	}

	IEnumerator BackToFullSpeed (float resetEnemySpeed, float timeOver)
	{
		yield return new WaitForSeconds (timeOver);
		enemySpeed = resetEnemySpeed;
	}

	public virtual void TakeDamageOverTime(float damageAmount, float timeOver)
	{
		isDamageOverTime = true;
		magicTimer = timeOver;
		damageOverTimeAmount = damageAmount;
	}

	protected virtual void OnHitByRanged(RangedIcon rangedIcon)
	{
		rangedIcon.EquippedRanged.DealDamage(this);
		Destroy(rangedIcon.gameObject);
	}

	protected virtual void OnHitByMagic(MagicIcon magicIcon)
	{
		magicIcon.EquippedMagic.DealDamage(this);
		Destroy(magicIcon.gameObject);
	}

	protected virtual void OnHitByMelee(WeaponControl control)
	{
		Color collideColor = new Color (255, 0, 0, 255);
		StartCoroutine(Flash(collideColor));
		control.Weapon.DealDamage(this);
	}
}
