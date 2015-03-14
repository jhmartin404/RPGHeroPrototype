using UnityEngine;
using System.Collections;

public class Boss : Enemy 
{
	public float specialAttackTime;
	protected float specialAttackTimer=0.0f;
	protected OnState specialAttack;//delegate for specialattack state
	// Use this for initialization
	public override void Start () 
	{
		base.Start ();
		specialAttack = OnSpecialAttack;
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		if(LevelStateManager.GetCurrentState() == LevelState.Running)
		{
			specialAttackTimer += Time.deltaTime;
			if(specialAttackTimer > specialAttackTime && enemyHealth>0 && (fsm.GetCurrentState() != specialAttack ||fsm.GetCurrentState() != attack))
			{
				fsm.PushState(specialAttack);//Switch to special attack state
				fsm.DoState();
			}
			base.Update ();
		}
	}

	protected virtual void OnSpecialAttack()
	{

		//Temporary for now
		specialAttackTimer = 0.0f;
		fsm.PopState ();
	}
}
