using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageText : MonoBehaviour 
{
	private Color col = new Color(1.0f, 1.0f, 0.0f, 0.0f);
	private float offset = 0.0f;  // scrolling velocity
	private float duration = 1.5f; // time to die
	private float alphaColor = 1.0f;
	private Text text;
	private bool fadeIn,fadeOut;
	private GameObject parent;
	private Renderer parentRenderer;

	void Start()
	{
		text = GetComponent<Text>();
		text.color = col; // set text color
		text.fontSize = 0;
		fadeIn = true;
		fadeOut = false;
	}

	void FadeIn()
	{
		Color textColor = text.color;
		textColor.a += Time.deltaTime;
		text.color = textColor; 
		if(text.fontSize < 25)
			text.fontSize++;

		if(text.color.a >= 1.0f)
		{
			fadeIn = false;
			fadeOut = true;
		}
	}
	
	void FadeOut()
	{
		Color textColor = text.color;
		textColor.a -= Time.deltaTime;
		text.color = textColor;   
		if(text.fontSize > 1)
			text.fontSize--;

		if(text.color.a <= 0.0f)
		{
			text.fontSize--;
			fadeIn = false;
			fadeOut = false;
			Destroy (gameObject);
		}
	}

	public void SetParent (GameObject par)
	{
		parent = par;
		parentRenderer = parent.GetComponent<Renderer> ();
	}

	void Update()
	{
		if(parent != null)
		{
			Vector3 pos = parent.transform.position;
			offset += 0.6f*Time.deltaTime;
			if(parentRenderer!=null)
				pos.y += offset+parentRenderer.bounds.extents.y;
			else
				pos.y += offset;
			transform.position = pos;

		}
		else
		{
			Destroy(gameObject);
		}

		if(fadeIn)
		{
			FadeIn();
		}
		else if(fadeOut)
		{
			FadeOut();
		}
	}
}
