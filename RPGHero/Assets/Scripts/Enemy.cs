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
	protected float xDirection = 1.0f;
	protected float yDirection = 0.0f;
	protected float attackDistance = 4.5f;
	protected float sizeChangeSpeed = 1.0f;
	protected float enemySpeed;
	protected EnemyType enemyType;
	protected float enemyHealth;
	protected float enemyAttackDamage;
	protected float attackTime=5.0f;
	private float attackTimer = 0.0f;
	protected float enemyYPosition;

	protected Vector2 movement;
	
	protected int expGiven;
	protected bool isDead;
	protected GameObject enemySpawner;
	protected bool isAttacking;
	protected Vector3 actionAreaCenter;
	protected Vector3 size;

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
		rigidbody2D.isKinematic = true;
		actionAreaCenter = GameObject.Find ("ActionArea").transform.renderer.bounds.center;
		enemySpawner = GameObject.Find ("EnemySpawner");
		size = transform.localScale;
		enemyType = EnemyType.Bandit;
		enemyHealth = 50;
		enemyAttackDamage = 10;
		enemySpeed = 5.0f;
		expGiven = 50;
		enemyYPosition = transform.position.y;
		isAttacking = false;
		isDead = false;
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
		attackTimer += Time.deltaTime;
		//Move left to right
		if(!isAttacking)
		{
			Move ();
		}
		//Move up down
		else if(isAttacking)
		{
			Attack();
		}
		if (Camera.main.WorldToViewportPoint (this.transform.position).x < 0.15) 
		{
			xDirection = 1;
		}
		else if(Camera.main.WorldToViewportPoint (this.transform.position).x > 0.85)
		{
			xDirection = -1;
		}
		
		//if(attackTimer > attackTime-0.5f)
		//{
		//	renderer.material.color = attackColor; //Warn the player that the enemy is about to attack
		//}

		if(enemyHealth < 25.0f)
		{
			LowHealth();
		}

		if(attackTimer > attackTime && enemyHealth>0 && !isAttacking)
		{
			isAttacking = true;
			yDirection = -1.0f;
		}
		
		transform.Translate (movement);

		if(enemyHealth<=0)
		{
			if(!isDead)
				Die();
		}
	}

	protected virtual void Move()
	{
		movement.y = 0;
		movement.x = xDirection * enemySpeed * Time.deltaTime;
	}

	protected virtual void Attack()
	{
		movement.x = 0;
		movement.y = yDirection * enemySpeed * Time.deltaTime;
		transform.localScale += size*sizeChangeSpeed*Time.deltaTime;
		if(transform.position.y <= actionAreaCenter.y && sizeChangeSpeed >0)
		{
			yDirection = 1.0f;
			sizeChangeSpeed = -1.0f;
			//StartCoroutine(AttackedPlayer());//Flash red screen
			if(!Player.Instance.IsDefending)
			{
				Player.Instance.Health -= enemyAttackDamage;
			}
			else if(Player.Instance.IsDefending)
			{
				Player.Instance.GetPlayerInventory().EquippedShield.BlockDamage(enemyAttackDamage);
			}
		}
		
		if(yDirection>= 0.0f && transform.position.y >= enemyYPosition && sizeChangeSpeed <0)
		{
			isAttacking = false;
			sizeChangeSpeed = 1.0f;
			attackTimer = 0;
			//renderer.material.color = normalColor;
		}
	}

	protected virtual void LowHealth()
	{
		attackTime = 3.0f;
	}

	protected virtual void Die()
	{
		Player.Instance.GetPlayerStats ().CurrentExp += expGiven;
		isDead = true;
		enemySpawner.GetComponent<EnemySpawner> ().NotifyEnemyDied ();
		Destroy (gameObject);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Magic")
		{
			MagicIcon magicIcon = other.gameObject.GetComponent<MagicIcon>();
			if(magicIcon.State == IconState.Thrown)
			{
				magicIcon.EquippedMagic.DealDamage(this.gameObject);
				Destroy(other.gameObject);
			}
		}
		else if(other.gameObject.tag == "Ranged")
		{
			RangedIcon rangedIcon = other.gameObject.GetComponent<RangedIcon>();
			if(rangedIcon.State == IconState.Thrown)
			{
				rangedIcon.EquippedRanged.DealDamage(this.gameObject);
				Destroy(other.gameObject);
			}
		}
		if(other.gameObject.tag == "Melee")
		{
			Debug.Log("Enter Collision");
			WeaponControl control = GameObject.Find("WeaponControl").GetComponent<WeaponControl>();
			if(control.CntrlState == ControlState.Active)
			{
				Debug.Log("Damage Done");
				control.Weapon.DealDamage(this.gameObject);
			}
		}
	}
	void OnCollisionStay2D(Collision2D other)
	{
		if(other.gameObject.tag == "Melee")
		{
			Debug.Log("Stay Collision");
			WeaponControl control = GameObject.Find("WeaponControl").GetComponent<WeaponControl>();
			if(control.CntrlState == ControlState.Active)
			{
				Debug.Log("Damage Done");
				control.Weapon.DealDamage(this.gameObject);
			}
		}
	}
}
