using UnityEngine;
using System.Collections;

public class MagicIcon : Icon 
{
	private Object fireBlastParticleSystemPrefab;
	private GameObject fireBlastParticleSystem;
	private GameObject actionArea;
	private Magic equippedMagic;

	public Magic EquippedMagic
	{
		get
		{
			return equippedMagic;
		}
		set
		{
			equippedMagic = value;
		}
	}
	// Use this for initialization
	public override void Start () 
	{
		base.Start ();
		fireBlastParticleSystemPrefab = Resources.Load ("Prefabs/FireBlastParticles");
		//equippedMagic = Player.Instance.GetPlayerInventory ().EquippedMagic1;
		gameObject.GetComponent<SpriteRenderer> ().sprite = equippedMagic.GetItemImage ();
		actionArea = GameObject.Find ("ActionArea");
		iconType = IconType.Magic;
	}

	protected override bool OnCheckSelected()
	{
		return Player.Instance.Mana >= equippedMagic.ManaCost;
	}

	protected override void OnIconTouched()
	{
		base.OnIconTouched ();
		actionArea.GetComponent<Renderer>().enabled = true;
		fireBlastParticleSystem = Instantiate(fireBlastParticleSystemPrefab, transform.position, transform.rotation) as GameObject;
		fireBlastParticleSystem.GetComponent<ParticleSystem>().Play();
	}
	
	protected override void OnIconLetGo()
	{
		base.OnIconLetGo ();
		actionArea.GetComponent<Renderer>().enabled = false;
		endPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		Player.Instance.Mana -= equippedMagic.ManaCost;
	}

	protected override void OnGrabbedState()
	{
		base.OnGrabbedState ();
		fireBlastParticleSystem.transform.position = transform.position;
	}

	public override void OnDestroy()
	{
		Destroy (fireBlastParticleSystem);
		Destroy (gameObject);
	}
}
