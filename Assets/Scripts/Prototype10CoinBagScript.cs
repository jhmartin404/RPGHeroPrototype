using UnityEngine;
using System.Collections;

public class Prototype10CoinBagScript : MonoBehaviour 
{
	public GameObject repairPrefab;
	private bool coinCollected;
	private Vector3 size = new Vector3(1,1,0);
	private Vector3 maxSize = new Vector3(1.05f,1.05f,0);
	private Vector3 minSize;
	private float sizeChangeSpeed = 0.2f;
	// Use this for initialization
	void Start () 
	{
		coinCollected = false;
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
			
			if(sizeChangeSpeed <0 && transform.localScale.sqrMagnitude <= size.sqrMagnitude)
			{
				sizeChangeSpeed = 0.2f;
				coinCollected = false;
				//transform.localScale = size;
			}
		}

		if(Prototype10Layout.getCoins() >=5)
		{
			GameObject repair = Instantiate(repairPrefab,transform.position,Quaternion.identity) as GameObject;
			Prototype10Layout.setCoins(0);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Coin")
		{
			//Add to coin count, set coinCollected to true and destroy the coin
			Prototype10Layout.addCoin();
			coinCollected = true;
			Destroy(other.gameObject);
		}
	}
}
