using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LightningBolt : MonoBehaviour 
{
	public Image lightningBolt;
	private bool retract;
	private float moveAmount = 3.5f;
	private GameObject parent;
	private Renderer parentRenderer;
	// Use this for initialization
	void Start () 
	{
		retract = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(parent != null)
		{
			Vector3 pos = parent.transform.position;
			if(parentRenderer!=null)
				pos.y += parentRenderer.bounds.extents.y;
			transform.position = pos;
		}
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

	public void SetParent (GameObject par)
	{
		parent = par;
		parentRenderer = parent.GetComponent<Renderer> ();
	}

}
