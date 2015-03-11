﻿using UnityEngine;
using System.Collections;

public class MagicIcon : Icon 
{
	private Object fireBlastParticleSystemPrefab;
	private Object frostBlastParticleSystemPrefab;
	private GameObject particleSystem;
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
		frostBlastParticleSystemPrefab = Resources.Load ("Prefabs/FrostBlastParticles");
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
		if(equippedMagic.GetType() == typeof(FireBlastMagic))
		{
			particleSystem = Instantiate(fireBlastParticleSystemPrefab, transform.position, transform.rotation) as GameObject;
			particleSystem.GetComponent<ParticleSystem>().Play();
		}
		else if(equippedMagic.GetType() == typeof(FrostBlastMagic))
		{
			particleSystem = Instantiate(frostBlastParticleSystemPrefab, transform.position, transform.rotation) as GameObject;
			particleSystem.GetComponent<ParticleSystem>().Play();
		}

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
		if(particleSystem != null)
		{
			particleSystem.transform.position = transform.position;
		}
	}

	public override void OnDestroy()
	{
		Destroy (particleSystem);
		Destroy (gameObject);
	}
}
