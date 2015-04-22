using UnityEngine;
using System.Collections;

public class RepairHammer : InventoryItem
{
	private float repairAmount;

	public RepairHammer()
	{
		
	}
	
	public RepairHammer(float amt, ItemType item, int id, string name, string imagePath, int cost, bool purchase) : base(item, id, name, imagePath, cost, purchase)
	{
		repairAmount = amt;
	}

	public float GetRepairAmount()
	{
		return repairAmount;
	}

	public override string ToString()
	{
		string result = base.ToString ();
		result += "Fully Repairs Shield\n";
		return result;
	}
}
