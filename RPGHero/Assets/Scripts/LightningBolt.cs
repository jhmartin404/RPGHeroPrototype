using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LightningBolt : MonoBehaviour 
{
	public Image lightningBolt;
	private bool retract;
	private float moveAmount = 3.5f;
	// Use this for initialization
	void Start () 
	{
		retract = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!retract)
			lightningBolt.fillAmount += moveAmount * Time.deltaTime;
		else if(retract)
			lightningBolt.fillAmount -= moveAmount * Time.deltaTime;
		if(lightningBolt.fillAmount >= 1.0f && !retract)
		{
			retract = true;
		}
		else if(lightningBolt.fillAmount <= 0 && retract)
		{
			retract = false;
			Destroy(gameObject);
		}
	}
}
