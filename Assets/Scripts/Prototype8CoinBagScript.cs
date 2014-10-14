using UnityEngine;
using System.Collections;

public class Prototype8CoinBagScript : MonoBehaviour 
{
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
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Coin")
		{
			Prototype8Layout.addCoin();
			coinCollected = true;
			Destroy(other.gameObject);
		}
	}
}
