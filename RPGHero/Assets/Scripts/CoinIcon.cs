using UnityEngine;
using System.Collections;

public class CoinIcon : Icon 
{
	private TrailRenderer trailRenderer;
	// Use this for initialization
	public override void Start () 
	{
		base.Start ();
		iconType = IconType.Coin;
		trailRenderer = gameObject.GetComponent<TrailRenderer> ();
		trailRenderer.enabled = false;
	}

	protected override void OnIconLetGo ()
	{
		base.OnIconLetGo ();
		trailRenderer.enabled = true;
	}
}
