using UnityEngine;
using System.Collections;

public class Common
{
	public float fingerRadius = 0.5f;

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

	public enum ItemType
	{
		Weapon,
		Shield,
		Magic,
		Misc
	};

	public enum EnemyType
	{
		Bandit,
		Bear,
		MountainLion,
		Eagle,
		Wolf,
		Skeleton,
		Orc,
		Boss
	};
}
