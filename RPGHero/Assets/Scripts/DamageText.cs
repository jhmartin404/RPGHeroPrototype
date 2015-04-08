using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageText : MonoBehaviour 
{

	private Color col = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	private float scroll = 1.0f;  // scrolling velocity
	private float duration = 1.5f; // time to die
	private float alpha;
	private Text text;

	void Start()
	{
		text = GetComponent<Text>();
		text.color = col; // set text color
		alpha = 1.0f;
	}
	
	void Update()
	{
		if (alpha > 0)
		{
			Vector3 pos = transform.position;
			pos.y += scroll*Time.deltaTime;
			transform.position = pos; 
			alpha -= Time.deltaTime/duration;
			Color textColor = text.color;
			textColor.a = alpha;
			text.color = textColor;        
		} 
		else 
		{
			Destroy(gameObject); // text vanished - destroy itself
		}
	}
}
