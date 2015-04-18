using UnityEngine;
using System.Collections;

public class MagicIcon : Icon 
{
	private Object fireEffectTrail;
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
		fireEffectTrail = Resources.Load ("Prefabs/FireEffectTrail");
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
		return Player.Instance.Mana >= magicManaCost;
	}

	protected override void OnIconTouched()
	{
		base.OnIconTouched ();
		actionArea.GetComponent<Renderer>().enabled = true;
		if(equippedMagic.GetType() == typeof(FireBlastMagic))
		{
			source.clip = fireSound;
			source.Play();
			GameObject fireEffect = Instantiate(fireEffectTrail, transform.position, transform.rotation) as GameObject;
			fireEffect.transform.SetParent(transform);
		}
		else if(equippedMagic.GetType() == typeof(FrostBlastMagic))
		{
			source.clip = iceSound;
			source.Play();
			magicParticleSystem = Instantiate(frostBlastParticleSystemPrefab, transform.position, transform.rotation) as GameObject;
			magicParticleSystem.transform.SetParent(transform);
			if(!magicParticleSystem.GetComponent<ParticleSystem>().isPlaying)
				magicParticleSystem.GetComponent<ParticleSystem>().Play();
		}
		else if(equippedMagic.GetType() == typeof(LightningMagic))
		{
			source.clip = lightningSound;
			source.Play();
			magicParticleSystem = Instantiate(lightningMagicPrefab, transform.position, transform.rotation) as GameObject;
			magicParticleSystem.transform.SetParent(transform);
		}

	}
	
	protected override void OnIconLetGo()
	{
		base.OnIconLetGo ();
		actionArea.GetComponent<Renderer>().enabled = false;
		endPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		Player.Instance.Mana -= magicManaCost;
	}

	public override void OnDestroy()
	{
		Destroy (magicParticleSystem);
		Destroy (gameObject);
	}
}
