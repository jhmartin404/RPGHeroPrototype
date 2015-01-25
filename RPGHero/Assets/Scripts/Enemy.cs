using UnityEngine;
using System.Collections;

public class Enemy 
{
	private Common.EnemyType enemyType;
	private float enemyHealth;
	private Sprite enemySprite;
	private int expGiven;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void Move()
	{

	}

	public virtual void Attack()
	{

	}

	public virtual void LowHealth()
	{

	}

	public virtual void Die()
	{
	
	}
}
