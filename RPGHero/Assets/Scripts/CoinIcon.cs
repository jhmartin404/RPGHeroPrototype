using UnityEngine;
using System.Collections;

public class CoinIcon : Icon 
{

	// Use this for initialization
	public override void Start () 
	{
		base.Start ();
		iconType = IconType.Coin;
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		base.Update ();
	}

	public override void LateUpdate()
	{
		base.LateUpdate ();
	}
}
