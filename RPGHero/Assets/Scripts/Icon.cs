using UnityEngine;
using System.Collections;

public enum IconType
{
	Magic,
	Coin,
	Ranged
};

public enum IconState
{
	ROTATING,
	GRABBED,
	THROWN
};

public class Icon 
{
	private const float fingerRadius = 0.5f;
	private Sprite iconImage;
	private IconType iconType;
	private Transform center;
	private float degressPerSecond;
	private IconState iconState;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
