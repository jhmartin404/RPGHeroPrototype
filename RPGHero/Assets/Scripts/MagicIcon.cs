using UnityEngine;
using System.Collections;

public class MagicIcon : Icon 
{
	private Object fireBlastParticleSystemPrefab;
	private Object frostBlastParticleSystemPrefab;
	private GameObject magicParticleSystem;
	private GameObject actionArea;
	private Magic equippedMagic;
	private float magicManaCost;

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
		frostBlastParticleSystemPrefab = Resources.Load ("Prefabs/FrostBlastParticles");
		gameObject.GetComponent<SpriteRenderer> ().sprite = equippedMagic.GetItemImage ();
		actionArea = GameObject.Find ("ActionArea");
		iconType = IconType.Magic;
		magicManaCost = equippedMagic.GetManaCost();
	}

	protected override bool OnCheckSelected()
	{
		return Player.Instance.Mana >= magicManaCost;//equippedMagic.ManaCost;
	}

	protected override void OnIconTouched()
	{
		base.OnIconTouched ();
		actionArea.GetComponent<Renderer>().enabled = true;
		if(equippedMagic.GetType() == typeof(FireBlastMagic))
		{
			magicParticleSystem = Instantiate(fireBlastParticleSystemPrefab, transform.position, transform.rotation) as GameObject;
			if(!magicParticleSystem.GetComponent<ParticleSystem>().isPlaying)
				magicParticleSystem.GetComponent<ParticleSystem>().Play();
		}
		else if(equippedMagic.GetType() == typeof(FrostBlastMagic))
		{
			magicParticleSystem = Instantiate(frostBlastParticleSystemPrefab, transform.position, transform.rotation) as GameObject;
			if(!magicParticleSystem.GetComponent<ParticleSystem>().isPlaying)
				magicParticleSystem.GetComponent<ParticleSystem>().Play();
		}

	}
	
	protected override void OnIconLetGo()
	{
		base.OnIconLetGo ();
		actionArea.GetComponent<Renderer>().enabled = false;
		endPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		Player.Instance.Mana -= magicManaCost;//equippedMagic.ManaCost;
	}

	protected override void OnGrabbedState()
	{
		base.OnGrabbedState ();
		if(magicParticleSystem != null)
		{
			magicParticleSystem.transform.position = transform.position;
		}
	}

	public override void OnDestroy()
	{
		Destroy (magicParticleSystem);
		Destroy (gameObject);
	}
}
