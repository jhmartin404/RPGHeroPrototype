using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Sound Effect Order
/// Index 0 is Hit Sound Effect
/// Index 1 is Move Sound Effect
/// Index 2 is Attack Sound Effect
/// Index 3 is Death Sound Effect
/// </summary>
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

public enum EnemySoundEffect
{
	EnemyHit = 0,
	EnemyStand = 1,
	EnemyAttack = 2,
	EnemyDeath = 3,
	EnemySpecialAttack = 4
};

public class Enemy : MonoBehaviour
{
	public AudioSource soundSource;
	public List<AudioClip> enemySoundEffects;

	public float enemySpeed = 5.0f;

	public EnemyType enemyType;
	public float enemyHealth = 50.0f;
	private float fullEnemyHealth;
	private float lowEnemyHealth;
	public float enemyAttackDamage = 10.0f;
	public float attackTime = 5.0f;
	public int expGiven = 50;

	private Vector3 Layer1 = new Vector3(2.0f,1.1f,1.0f);
	private Vector3 Layer2 = new Vector3(2.2f,1.2f,0.8f);
	private Vector3 Layer3 = new Vector3(2.4f,1.3f,0.7f);
	private Vector3 Layer4 = new Vector3(2.6f,1.35f,0.6f);
	private Vector3 Layer5 = new Vector3(2.8f,1.4f,0.5f);
	private Vector3 Layer6 = new Vector3(3.0f,1.45f,0.4f);

	protected OnState move;//delegate for moving state
	protected OnState stand;//delegate for standing state
	protected OnState attack;//delegate for attack state
	protected OnState lowHealth;//delegate for lowHealth state
	protected OnState die;//delegate for die state
	protected FiniteStateMachine fsm;//fsm to keep track of the enemy's state

	private Renderer enemyRenderer;

	protected float xDirection = MOVE_RIGHT;
	private float enemySizeChange;
	protected float attackTimer = 0.0f;
	private float magicTimer = 0.0f; //timer used to manage magic effects
	private float slowedTimer = 0.0f; //timer used for managing enemy slowdown
	private float resetEnemySpeed; //Stores the enemy's original speeed before slowing down
	private float resetEnemySizeChange; //Stores the enemy's original size change before slowing down
	protected float moveTimer; //Timer for handling enemy movement
	protected float standTimer; //Timer for enemy standing
	protected Vector3 enemyPosition;//Store y position before an attack
	private GameObject enemyHealthBar;
	private Image enemyHealthBarImage;
	private Object damageTextPrefab;
	private GameObject damageText;

	//For damage over time
	private float damageOverTimeAmountPerSec;
	private float damageOverTimeTotalDamage;
	private float damageOverTimeAccumulator;

	protected Vector2 movement;

	protected GameObject enemySpawner;
	protected Vector3 actionAreaCenter;
	protected Vector3 size;
	protected Vector3 minSize;
	protected Vector3 maxSize;
	protected Color normalColor;

	private Object iceEffectPrefab;
	private GameObject iceEffectGameObject;
	private AudioClip iceEffectSound;
	private Object fireEffectPrefab;
	private GameObject fireEffectGameObject;
	private AudioClip fireEffectSound;
	private GameObject electrocutedGameObject;
	private AudioClip electrocutedSound;

	private float startTime;
	private float sizeLength;
	private Vector3 stoppedSize;

	private bool isSlowed;
	private bool isDamageOverTime;
	private bool isLowHealth;
	private bool movingIn;

	private List<Vector3> layers = new List<Vector3>();

	private const float MOVE_RIGHT = 1.0f;
	private const float MOVE_LEFT = -1.0f;

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

	public void Awake()
	{
		layers.Add (Layer1);
		layers.Add (Layer2);
		layers.Add (Layer3);
		layers.Add (Layer4);
		layers.Add (Layer5);
		layers.Add (Layer6);
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

		enemyRenderer = GetComponent<Renderer> ();

		soundSource = GetComponent<AudioSource> ();

		lowEnemyHealth = enemyHealth * 0.25f;

		iceEffectPrefab = Resources.Load("Prefabs/IceBlock");
		iceEffectSound = Resources.Load<AudioClip> ("IceEffectSound");
		fireEffectPrefab = Resources.Load ("Prefabs/FireEffect");
		fireEffectSound = Resources.Load<AudioClip> ("FireEffectSound");
		electrocutedSound = Resources.Load<AudioClip> ("ElectrocutedSound");

		GetComponent<Rigidbody2D>().isKinematic = true;
		actionAreaCenter = GameObject.Find ("ActionArea").transform.GetComponent<Renderer>().bounds.center;
		enemySpawner = GameObject.Find ("EnemySpawner");

		size = transform.localScale;
		enemySizeChange = 2.5f;
		movingIn = true;

		enemyPosition = transform.position;

		normalColor = enemyRenderer.material.color;
		fullEnemyHealth = enemyHealth;

		moveTimer = Random.Range (2, 5);
		standTimer = Random.Range (1, 3);
		isSlowed = false;
		isDamageOverTime = false;
		isLowHealth = false;

		Object healthBar = Resources.Load ("Prefabs/EnemyHealthBar");
		enemyHealthBar = Instantiate (healthBar, transform.position, transform.rotation) as GameObject;
		enemyHealthBar.transform.SetParent (GameObject.Find ("Canvas").transform, false);
		enemyHealthBar.transform.position = transform.position;
		enemyHealthBarImage = enemyHealthBar.GetComponentsInChildren<Image> ()[1];
		damageTextPrefab = Resources.Load ("Prefabs/DamageText");
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
		if(LevelStateManager.GetCurrentState() == LevelState.Running)
		{
			attackTimer += Time.deltaTime;
		
			//Move left to right
			if(fsm.GetCurrentState() == null)
			{
				fsm.PushState(move);//Player should always at least be moving if they have no state
			}

			if(enemyHealth <= lowEnemyHealth && !isLowHealth)
			{
				fsm.PushState(lowHealth);
				fsm.DoState();
				isLowHealth = true;
			}

			if(attackTimer > attackTime && enemyHealth>0 && fsm.GetCurrentState() == move)
			{
				startTime = Time.time;
				sizeLength = Vector3.Distance (minSize, maxSize);
				enemyPosition = transform.position;
				fsm.PushState(attack);//Switch to attack state
			}

			if(fsm.GetCurrentState() != attack)
			{
				transform.Translate (movement);
			}

			if(isDamageOverTime)
			{
				fireEffectGameObject.transform.position = transform.position;
				soundSource.clip = fireEffectSound;
				if(!soundSource.isPlaying)
					soundSource.Play();
				magicTimer -= Time.deltaTime;
				enemyHealth -= damageOverTimeAmountPerSec*Time.deltaTime;
				damageOverTimeTotalDamage -= damageOverTimeAmountPerSec*Time.deltaTime;
				damageOverTimeAccumulator += damageOverTimeAmountPerSec*Time.deltaTime;
				if(damageOverTimeAccumulator >= 2.0f)
				{
					SpawnDamageText((int)-damageOverTimeAccumulator);
					damageOverTimeAccumulator = 0;
				}
				if(magicTimer<=0)
				{
					SpawnDamageText((int)-damageOverTimeAccumulator);
					damageOverTimeAccumulator = 0;
					damageOverTimeTotalDamage = 0;
					Destroy(fireEffectGameObject);
					soundSource.Stop();
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
				if(fsm.GetCurrentState() != stand)
				{
					PlayEnemySoundEffect((int)EnemySoundEffect.EnemyStand);
					fsm.PushState(stand);
				}
			}

			Debug.Log("State: " +fsm.GetCurrentState ().Method.Name);//used for testing
			fsm.DoState ();//Run the method associated with the current state the enemy is in
			if(enemyHealth>0)
			{
				UpdateEnemyHealthBar();
			}

			if(isSlowed)
			{
				slowedTimer -= Time.deltaTime;
				iceEffectGameObject.transform.position = transform.position;
				if(slowedTimer <= 0)
				{
					Destroy(iceEffectGameObject);
					isSlowed = false;
					slowedTimer = 0.0f;
					enemySpeed = resetEnemySpeed;
					enemySizeChange = resetEnemySizeChange;
				}
			}
		}
	}

	private void SpawnDamageText(float damage)
	{
		damageText = Instantiate (damageTextPrefab, transform.position,Quaternion.identity)as GameObject;
		damageText.GetComponent<Text> ().text = damage.ToString ();
		damageText.transform.SetParent (GameObject.Find ("Canvas").transform, false);
		damageText.GetComponent<DamageText> ().SetParent (gameObject);
	}

	private void UpdateEnemyHealthBar()
	{
		Vector3 pos = transform.position;
		pos.y += enemyRenderer.bounds.extents.y;
		enemyHealthBar.transform.position = pos; 
		enemyHealthBarImage.fillAmount = enemyHealth / fullEnemyHealth;
	}

	public void SetLayer(float xVal,int layer)
	{
		Vector3 vec = layers [layer - 1];
		transform.position = new Vector3(xVal,vec.x,vec.y);
		maxSize = transform.localScale * 1.5f;
		transform.localScale *= vec.z;
		minSize = transform.localScale;
	}

	//Flash enemy's color, used to show when enemy is hit
	protected IEnumerator Flash(Color collideColor)
	{
		enemyRenderer.material.color = collideColor;
		yield return new WaitForSeconds(0.1f);
		enemyRenderer.material.color = normalColor;
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
		float attackLength = Vector3.Distance (enemyPosition, actionAreaCenter);
		//float attackLengthCovered = 0, sizeLengthCovered = 0;
		//if(!isSlowed)
		//{
		float attackLengthCovered = (Time.time - startTime) * enemySpeed;
		float sizeLengthCovered = (Time.time - startTime) * enemySizeChange;
		//}
		//else if (isSlowed)
		//{
		//	attackLengthCovered = (Time.time - startTime) * resetEnemySpeed;
		//	sizeLengthCovered = (Time.time - startTime) * resetEnemySizeChange;
		//}

		float attackFraction = attackLengthCovered / attackLength;
		float sizeFraction = sizeLengthCovered / sizeLength;
		if(movingIn)
		{
			transform.localScale = Vector3.Lerp(minSize,maxSize,sizeFraction);
			transform.position = Vector3.Lerp(enemyPosition,actionAreaCenter,attackFraction);
		}
		else
		{
			transform.localScale = Vector3.Lerp(stoppedSize,minSize,sizeFraction);
			transform.position = Vector3.Lerp(actionAreaCenter,enemyPosition,attackFraction);
		}

		if(Vector2.Distance(transform.position,actionAreaCenter) <= 0 && movingIn)
		{
			movingIn = false;
			startTime = Time.time;
			if(Player.Instance.TakeDamage(this))
			{
				PlayEnemySoundEffect((int)EnemySoundEffect.EnemyAttack);
			}
			sizeLength = Vector3.Distance (transform.localScale, minSize);
			stoppedSize = transform.localScale;
		}
		else if(Vector2.Distance(transform.position,enemyPosition)<=0 && !movingIn)
		{
			movingIn=true;
			transform.localScale = minSize;
			attackTimer = 0;
			fsm.PopState();//when attack is finished pop the state off the stack
		}
	}

	protected virtual void OnLowHealth()
	{
		attackTime = attackTime * 0.5f;
		fsm.PopState ();
	}

	protected virtual void OnDie()
	{
		if(damageText != null)
		{
			Destroy(damageText);
		}

		int statIncreased = Player.Instance.GetPlayerStats ().WisdomStat - 50;
		int amountAdded = (int)(statIncreased * 0.2);

		bool leveledUp = Player.Instance.AddExperience(expGiven+amountAdded);
		GameObject exp = Instantiate (damageTextPrefab, transform.position,Quaternion.identity)as GameObject;
		if(leveledUp)
		{
			exp.GetComponent<Text> ().text = "Leveled Up!";
		}
		else
		{
			exp.GetComponent<Text> ().text = (expGiven+amountAdded).ToString () + "Exp";
		}
		exp.transform.SetParent (GameObject.Find ("Canvas").transform, false);
		exp.GetComponent<DamageText> ().SetParent (GameObject.Find("TextLocation"));

		AudioSource.PlayClipAtPoint(GetSoundEffect((int)EnemySoundEffect.EnemyDeath), transform.position);
		enemySpawner.GetComponent<EnemySpawner> ().NotifyEnemyDied ();
		fsm.PopState ();
		Destroy (enemyHealthBar);
		Destroy (iceEffectGameObject);
		Destroy (fireEffectGameObject);
		Destroy (gameObject);
	}

	protected void PlayEnemySoundEffect(int index)
	{
		if(soundSource != null)
		{
			AudioClip sound = GetSoundEffect (index);
			if(sound != null && soundSource.clip != null && soundSource.clip.name == sound.name)
			{
				if(!soundSource.isPlaying)
					soundSource.Play();
			}
			else
			{
				if(sound != null)
				{
					soundSource.clip = sound;
					soundSource.Play();
				}
				else
				{
					Debug.LogError("Sound was not found");
				}
			}
		}
		else
		{
			Debug.Log("AudioSource does not exist");
		}
	}

	protected AudioClip GetSoundEffect(int index)
	{
		if(index < enemySoundEffects.Count && index >= 0)
		{
			return enemySoundEffects[index];
		}
		else
		{
			return null;
		}
	}

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		onHitEnter (other);
	}

	protected virtual void onHitEnter(Collider2D col)
	{
		if(col.gameObject.tag == "Melee")
		{
			WeaponControl control = GameObject.Find("WeaponControl").GetComponent<WeaponControl>();
			if(control.CntrlState == ControlState.Active)
			{
				OnHitByMelee(control);
			}
		}
	}

	protected virtual void OnTriggerStay2D(Collider2D other)
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
	}

	public virtual void TakeDamage(float damageAmount)
	{
		PlayEnemySoundEffect ((int)EnemySoundEffect.EnemyHit);
		enemyHealth -= damageAmount;
		SpawnDamageText (-damageAmount);
	}

	public virtual void SlowDown(float speedDecrease, float timeOver)
	{
		soundSource.clip = iceEffectSound;
		soundSource.Play ();
		if(iceEffectGameObject == null)
		{
			resetEnemySpeed = enemySpeed;
			resetEnemySizeChange = enemySizeChange;
			enemySpeed *= speedDecrease;
			enemySizeChange *= speedDecrease;
			iceEffectGameObject = Instantiate (iceEffectPrefab, transform.position, transform.rotation) as GameObject;
		}
		isSlowed = true;
		slowedTimer += timeOver;
	}

	public virtual void TakeDamageOverTime(float damageAmount, float timeOver)
	{
		if(fireEffectGameObject == null)
		{
			fireEffectGameObject = Instantiate (fireEffectPrefab, transform.position, transform.rotation) as GameObject;
		}
		isDamageOverTime = true;
		damageOverTimeTotalDamage += damageAmount;
		magicTimer += timeOver;
		damageOverTimeAmountPerSec = damageOverTimeTotalDamage/magicTimer;
	}

	protected virtual void OnHitByRanged(RangedIcon rangedIcon)
	{
		rangedIcon.EquippedRanged.DealDamage(this);
		rangedIcon.OnDestroy ();
	}

	protected virtual void OnHitByMagic(MagicIcon magicIcon)
	{
		if(magicIcon.EquippedMagic.GetType() == typeof(LightningMagic))
		{
			AudioSource.PlayClipAtPoint(electrocutedSound,transform.position);
		}
		magicIcon.EquippedMagic.DealDamage(this);
		magicIcon.OnDestroy ();
	}

	protected virtual void OnHitByMelee(WeaponControl control)
	{
		Color collideColor = new Color (255, 0, 0, 255);
		StartCoroutine(Flash(collideColor));
		control.Weapon.DealDamage(this);
	}
}
