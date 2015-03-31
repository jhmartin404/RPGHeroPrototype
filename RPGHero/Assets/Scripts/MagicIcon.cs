using UnityEngine;
using System.Collections;

public class MagicIcon : Icon 
{
	private Object fireBlastParticleSystemPrefab;
	private Object frostBlastParticleSystemPrefab;
	private Object lightningMagicPrefab;
	private GameObject magicParticleSystem;
	private GameObject actionArea;
	private Magic equippedMagic;
	private float magicManaCost;
	private AudioClip lightningSound;
	private AudioClip fireSound;
	private AudioClip iceSound;
	public AudioSource source;

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
		fireBlastParticleSystemPrefab = Resources.Load ("Prefabs/FireEffect");
		frostBlastParticleSystemPrefab = Resources.Load ("Prefabs/FrostBlastParticles");
		lightningMagicPrefab = Resources.Load ("Prefabs/LightningEffect");
		gameObject.GetComponent<SpriteRenderer> ().sprite = equippedMagic.GetItemImage ();
		actionArea = GameObject.Find ("ActionArea");
		iconType = IconType.Magic;
		magicManaCost = equippedMagic.GetManaCost();
		lightningSound = Resources.Load<AudioClip> ("LightningSound");
		fireSound = Resources.Load<AudioClip> ("FireEffectSound");
		iceSound = Resources.Load<AudioClip> ("IceSound");
		source = GetComponent<AudioSource> ();
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
			source.clip = fireSound;
			magicParticleSystem = Instantiate(fireBlastParticleSystemPrefab, transform.position, transform.rotation) as GameObject;
			if(!magicParticleSystem.GetComponent<ParticleSystem>().isPlaying)
				magicParticleSystem.GetComponent<ParticleSystem>().Play();
		}
		else if(equippedMagic.GetType() == typeof(FrostBlastMagic))
		{
			source.clip = iceSound;
			magicParticleSystem = Instantiate(frostBlastParticleSystemPrefab, transform.position, transform.rotation) as GameObject;
			if(!magicParticleSystem.GetComponent<ParticleSystem>().isPlaying)
				magicParticleSystem.GetComponent<ParticleSystem>().Play();
		}
		else if(equippedMagic.GetType() == typeof(LightningMagic))
		{
			source.clip = lightningSound;
			magicParticleSystem = Instantiate(lightningMagicPrefab, transform.position, transform.rotation) as GameObject;
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
			if(!source.isPlaying)
			{
				source.Play();
			}
			magicParticleSystem.transform.position = transform.position;
		}
	}

	protected override void OnThrownState()
	{
		if(magicParticleSystem != null)
		{
			if(!source.isPlaying)
			{
				source.Play();
			}
			magicParticleSystem.transform.position = transform.position;
		}
	}

	public override void OnDestroy()
	{
		Destroy (magicParticleSystem);
		Destroy (gameObject);
	}
}
