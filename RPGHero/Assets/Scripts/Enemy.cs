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
	protected OnState move;
	protected OnState attack;
	protected OnState lowHealth;
	protected OnState die;
	protected FiniteStateMachine fsm;
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
	protected float enemyYPosition;

	protected Vector2 movement;
	
	public int expGiven = 50;
	protected bool isDead;
	protected GameObject enemySpawner;
	protected bool isAttacking;
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
		isAttacking = false;
		isDead = false;
		normalColor = renderer.material.color;
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
		attackTimer += Time.deltaTime;
		//Move left to right
		if(fsm.GetCurrentState() != attack)
		{
			fsm.PushState(move);
			//OnMove ();
		}
		//Move up down
		//else if(isAttacking)
		//{
			//OnAttack();
		//}

		//if(enemyHealth <= lowEnemyHealth)
		//{
		//	fsm.PushState(lowHealth);
			//OnLowHealth();
		//}

		if(attackTimer > attackTime && enemyHealth>0 && fsm.GetCurrentState() != attack/*!isAttacking*/)
		{
			//isAttacking = true;
			fsm.PushState(attack);
			yDirection = -1.0f;
		}
		
		transform.Translate (movement);

		if(enemyHealth<=0)
		{
			if(!isDead)
			{
				fsm.PushState(die);
				//OnDie();
			}
		}

		Debug.Log("State: " +fsm.GetCurrentState ().Method.Name);
		fsm.DoState ();
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
			isAttacking = false;
			sizeChangeSpeed = 1.0f;
			attackTimer = 0;
			fsm.PopState();
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
		isDead = true;
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
			OnHitByMagic(col);
		}
		else if(col.gameObject.tag == "Ranged")
		{
			OnHitByRanged(col);
		}
		else if(col.gameObject.tag == "Melee")
		{
			OnHitByMelee(col);
		}
	}

	protected virtual void OnHitByRanged(Collider2D col)
	{
		RangedIcon rangedIcon = col.gameObject.GetComponent<RangedIcon>();
		if(rangedIcon.State == IconState.Thrown)
		{
			rangedIcon.EquippedRanged.DealDamage(this);
			Destroy(col.gameObject);
		}
	}

	protected virtual void OnHitByMagic(Collider2D col)
	{
		MagicIcon magicIcon = col.gameObject.GetComponent<MagicIcon>();
		if(magicIcon.State == IconState.Thrown)
		{
			magicIcon.EquippedMagic.DealDamage(this);
			Destroy(col.gameObject);
		}
	}

	protected virtual void OnHitByMelee(Collider2D col)
	{
		WeaponControl control = GameObject.Find("WeaponControl").GetComponent<WeaponControl>();
		if(control.CntrlState == ControlState.Active)
		{
			Color collideColor = new Color (255, 0, 0, 255);
			StartCoroutine(Flash(collideColor));
			control.Weapon.DealDamage(this);
		}
	}
}
