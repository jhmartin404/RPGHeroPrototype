using UnityEngine;
using System.Collections;

public class Prototype10EquippedShieldScript : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if not rendering the shield then set defending to false
		if(Prototype10Layout.getDefending() && !renderer.enabled)
		{
			Prototype10Layout.setDefending(false);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//when colliding with an enemy set defending to true
		if(other.gameObject.tag == "Enemy")
		{
			Prototype10Layout.setDefending(true);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		//when enemy leaves set defending back to false
		if(other.gameObject.tag == "Enemy")
		{
			Prototype10Layout.setDefending(false);
		}
	}
}
