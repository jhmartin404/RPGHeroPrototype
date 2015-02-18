using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum LevelState
{
	Start,
	Running,
	Lost,
	Won
};

public class LevelScript : MonoBehaviour 
{
	public static FiniteStateMachine fsm;
	public static event OnState OnLevelStartEvent;
	public static event OnState OnLevelRunningEvent;
	public static event OnState OnLevelLostEvent;
	public static event OnState OnLevelWonEvent;
	private PlayerStats playerStats;
	public Image healthBar;
	public Image staminaBar;
	public Image manaBar;
	private bool iconSelected;
	private GameObject enemySpawner;
	private GameObject weapon;

	public bool IconSelected
	{
		get
		{
			return iconSelected;
		}
		set
		{
			iconSelected = value;
		}
	}

	void Awake()
	{
		weapon = Instantiate(Player.Instance.GetPlayerInventory ().EquippedMeleeWeapon.MeleeWeaponPrefab,transform.position,Quaternion.identity) as GameObject;
		weapon.name = "Weapon";
		fsm = new FiniteStateMachine ();
	}

	// Use this for initialization
	void Start () 
	{
		playerStats = Player.Instance.GetPlayerStats ();

		healthBar.fillAmount = (float)(Player.Instance.Health/ (float)playerStats.HealthStat);

		staminaBar.fillAmount = (float)(Player.Instance.Stamina/ (float)playerStats.MaxStamina);

		manaBar.fillAmount = (float)(Player.Instance.Mana/ (float)playerStats.MaxMana);

		enemySpawner = GameObject.Find ("EnemySpawner");

		fsm.PushState (OnLevelStartEvent);
		fsm.DoState ();
		fsm.PushState (OnLevelRunningEvent);
	}
	
	// Update is called once per frame
	void Update () 
	{
		healthBar.fillAmount = (float)(Player.Instance.Health/ (float)playerStats.HealthStat);
		staminaBar.fillAmount = (float)(Player.Instance.Stamina/ (float)playerStats.MaxStamina);
		manaBar.fillAmount = (float)(Player.Instance.Mana/ (float)playerStats.MaxMana);

		if(Player.Instance.Health <=0)
		{
			enemySpawner.GetComponent<EnemySpawner>().NotifyPlayerDied();
			fsm.PushState(OnLevelLostEvent);
		}

		if(Application.platform == RuntimePlatform.Android)
		{
			if(Input.GetKey(KeyCode.Escape))
			{
				GoBack();
			}
		}

		fsm.DoState ();
	}

	public void addCoin()
	{
		Player.Instance.GetPlayerInventory().Coins++;
	}

	public void GoBack()
	{
		Application.LoadLevel ("LevelSelectScene");
	}
}
