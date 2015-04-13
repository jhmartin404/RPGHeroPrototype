﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinBag : MonoBehaviour 
{
	public Canvas canvas;
	public GameObject textLocation;
	private bool coinCollected;
	private Vector3 size;
	private Vector3 maxSize = new Vector3(1.0f,1.0f,0);
	private Vector3 minSize;
	private float sizeChangeSpeed = 0.2f;
	private AudioClip coinCollectedSound;
	private Object textPrefab;

	// Use this for initialization
	void Start () 
	{
		coinCollected = false;
		minSize = transform.localScale;
		size = minSize;
		coinCollectedSound = Resources.Load<AudioClip> ("CoinCollectedSound");
		textPrefab = Resources.Load ("Prefabs/DamageText");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(coinCollected)
		{
			//Grow then shrink coinbag when a coin is collected
			transform.localScale += size*sizeChangeSpeed*Time.deltaTime;
			if(transform.localScale.sqrMagnitude >= maxSize.sqrMagnitude && sizeChangeSpeed >0)
			{
				sizeChangeSpeed = -0.2f;
			}
			
			if(sizeChangeSpeed <0 && transform.localScale.sqrMagnitude <= minSize.sqrMagnitude)
			{
				sizeChangeSpeed = 0.2f;
				coinCollected = false;
			}
		}
	}

	void AddCoin()
	{
		Player.Instance.TemporaryCoins++;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Coin")
		{
			CoinIcon icon = col.GetComponent<CoinIcon>();
			if(icon.State == IconState.Thrown)
			{
				AudioSource.PlayClipAtPoint(coinCollectedSound,transform.position);
				AddCoin();
				GameObject text = Instantiate(textPrefab,transform.position,Quaternion.identity) as GameObject;
				text.transform.SetParent (canvas.transform, false);
				text.GetComponent<DamageText>().SetParent(textLocation);
				text.GetComponent<Text>().text = "+1";
				coinCollected = true;
				icon.OnDestroy();
			}
		}
	}
}
