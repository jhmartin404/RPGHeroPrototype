﻿using UnityEngine;
using UnityEngine.UI;
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
	Boss,
	Spider
};

public class Enemy : MonoBehaviour
{
	private const float MOVE_RIGHT = 1.0f;
	private const float MOVE_LEFT = -1.0f;
	protected OnState move;//delegate for moving state
	protected OnState stand;//delegate for standing state
	protected OnState attack;//delegate for attack state
	protected OnState lowHealth;//delegate for lowHealth state
	protected OnState die;//delegate for die state
	protected FiniteStateMachine fsm;//fsm to keep track of the enemy's state
	protected float xDirection = MOVE_RIGHT;
	protected float yDirection = 0.0f;
	protected float sizeChangeSpeed = 1.0f;
	public float enemySpeed = 5.0f;
	public EnemyType enemyType;
	public float enemyHealth = 50.0f;
	private float fullEnemyHealth;
	private float lowEnemyHealth;
	public float enemyAttackDamage = 10.0f;
	public float attackTime = 5.0f;
	protected float attackTimer = 0.0f;
	private float magicTimer = 0.0f; //timer used to manage magic effects
	private float moveTimer; //Timer for handling enemy movement
	private float standTimer; //Timer for enemy standing
	private float damageOverTimeAmount;
	protected Vector3 enemyPosition;//Store y position before an attack
	protected GameObject enemyHealthBar;
	protected Image enemyHealthBarImage;

	protected Vector2 movement;
	
	public int expGiven = 50;
	protected GameObject enemySpawner;
	protected bool isSlowed;
	protected bool isDamageOverTime;
	protected Vector3 actionAreaCenter;
	protected Vector3 size;
	protected Color normalColor;

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
		stand = OnStand;
		attack = OnAttack;
		lowHealth = OnLowHealth;
		die = OnDie;
		fsm = new FiniteStateMachine ();
		lowEnemyHealth = enemyHealth * 0.25f;
		rigidbody2D.isKinematic = true;
		actionAreaCenter = GameObject.Find ("ActionArea").transform.renderer.bounds.center;
		enemySpawner = GameObject.Find ("EnemySpawner");
		size = transform.localScale;
		enemyPosition = transform.position;
		normalColor = renderer.material.color;
		fullEnemyHealth = enemyHealth;
		moveTimer = Random.Range (2, 5);
		standTimer = Random.Range (1, 3);
		Object healthBar = Resources.Load ("Prefabs/EnemyHealthBar");
		enemyHealthBar = Instantiate (healthBar, transform.position, transform.rotation) as GameObject;
		enemyHealthBar.transform.SetParent (GameObject.Find ("Canvas").transform, false);
		enemyHealthBar.transform.position = transform.position;
		enemyHealthBarImage = enemyHealthBar.GetComponentsInChildren<Image> ()[1];
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
		attackTimer += Time.deltaTime;
		
		//Move left to right
		if(fsm.GetCurrentState() == null)
		{
			fsm.PushState(move);//Player should always at least be moving if they have no state
		}

		//if(enemyHealth <= lowEnemyHealth)
		//{
		//	fsm.PushState(lowHealth);
			//OnLowHealth();
		//}

		if(attackTimer > attackTime && enemyHealth>0 && fsm.GetCurrentState() == move)
		{
			enemyPosition = transform.position;
			fsm.PushState(attack);//Switch to attack state
			yDirection = -1.0f;
		}

		if(fsm.GetCurrentState() != attack)
		{
			transform.Translate (movement);
		}

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
			}
		}

		if(moveTimer <= 0)
		{
			fsm.PushState(stand);

		}

		Debug.Log("State: " +fsm.GetCurrentState ().Method.Name);//used for testing
		fsm.DoState ();//Run the method associate with the current state the enemy is in
		if(enemyHealth>0)
		{
			UpdateEnemyHealthBar();
		}
	}

	private void UpdateEnemyHealthBar()
	{
		Vector3 pos = transform.position;
		pos.y += renderer.bounds.extents.y;
		enemyHealthBar.transform.position = pos; 
		enemyHealthBarImage.fillAmount = enemyHealth / fullEnemyHealth;
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
			xDirection = MOVE_RIGHT;
		}
		else if(Camera.main.WorldToViewportPoint (this.transform.position).x > 0.85)
		{
			xDirection = MOVE_LEFT;
		}
		moveTimer -= Time.deltaTime;
	}

	protected virtual void OnStand()
	{
		movement.y = 0;
		movement.x = 0;
		standTimer -= Time.deltaTime;
		if(standTimer <= 0)
		{
			standTimer = Random.Range(1,3);
			moveTimer = Random.Range(2,5);
			xDirection = Random.Range(0,1)*2-1;
			fsm.PopState();
		}

	}

	protected virtual void OnAttack()
	{
		//Vector3 velocity = Vector3.zero;
		//float smoothTime = 0.3f;
		float step = enemySpeed * Time.deltaTime;
		transform.localScale += size*sizeChangeSpeed*Time.deltaTime;
		if(sizeChangeSpeed>0)
		{
			transform.position = Vector2.MoveTowards (transform.position, actionAreaCenter, step);
			//transform.position = Vector3.SmoothDamp(transform.position, actionAreaCenter, ref velocity, smoothTime);
		}
		else
		{
			transform.position = Vector2.MoveTowards (transform.position, enemyPosition, step);
			//transform.position = Vector3.SmoothDamp(transform.position, enemyPosition, ref velocity, smoothTime);
		}

		if(Vector2.Distance(transform.position,actionAreaCenter)<step)
		{
			yDirection = 1.0f;
			sizeChangeSpeed = -1.0f;
			Player.Instance.TakeDamage(this);
		}
		else if(Vector2.Distance(transform.position,enemyPosition)<step)
		{
			sizeChangeSpeed = 1.0f;
			attackTimer = 0;
			fsm.PopState();//when attack is finished pop the state off the stack
		}

		//if(transform.position.y + movement.y <= actionAreaCenter.y && sizeChangeSpeed >0)
		//{
		//	yDirection = 1.0f;
		//	sizeChangeSpeed = -1.0f;
		//	Player.Instance.TakeDamage(this);
		//}
		
		//if(yDirection>= 0.0f && transform.position.y + movement.y >= enemyYPosition && sizeChangeSpeed <0)
		//{
		//	sizeChangeSpeed = 1.0f;
		//	attackTimer = 0;
		//	fsm.PopState();//when attack is finished pop the state off the stack
		//}
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
		Destroy (enemyHealthBar);
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
		enemySpeed *= speedDecrease;
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
