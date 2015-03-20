using UnityEngine;
using System.Collections;

public class RepairHammer : InventoryItem
{
	
	public RepairHammer()
	{
		
	}
	
	public RepairHammer(ItemType item, int id, string name, string imagePath, int cost, bool purchase) : base(item, id, name, imagePath, cost, purchase)
	{

	}

	public override string ToString()
	{
		string result = base.ToString ();
		result += "Fully restores equipped shield.\n";
		return result;
	}
}
