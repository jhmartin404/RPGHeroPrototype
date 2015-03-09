using UnityEngine;
using System.Collections;

public class CoinBag : MonoBehaviour 
{
	private bool coinCollected;
	private Vector3 size;
	private Vector3 maxSize = new Vector3(1.0f,1.0f,0);
	private Vector3 minSize;
	private float sizeChangeSpeed = 0.2f;
	// Use this for initialization
	void Start () 
	{
		coinCollected = false;
		minSize = transform.localScale;
		size = minSize;
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
		Player.Instance.GetPlayerInventory ().Coins++;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Coin")
		{
			AddCoin();
			coinCollected = true;
			CoinIcon icon = col.GetComponent<CoinIcon>();
			icon.OnDestroy();
		}
	}
}
