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
	Rotating,
	Grabbed,
	Thrown
};

public class Icon : MonoBehaviour
{
	private const float fingerRadius = 0.5f;
	//private Sprite iconImage;
	private IconType iconType;
	private Transform center;
	private float degreesPerSecond;
	private IconState iconState;

	private Vector2 startPosition;//start position of the fireball
	private Vector2 endPosition;//end position of the fireball
	private Vector3 v;

	public Transform Center
	{
		get
		{
			return center;
		}
		set
		{
			center = value;
		}
	}

	public float DegreesPerSecond
	{
		get
		{
			return degreesPerSecond;
		}
		set
		{
			degreesPerSecond = value;
		}
	}

	// Use this for initialization
	void Start () 
	{
		iconState = IconState.Rotating;
		//center = GameObject.Find ("LeftCircle").transform;
		//degreesPerSecond = 85.0f;
		v = transform.position - center.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(iconState == IconState.Rotating)
		{
			Rotating();
		}
		else if(iconState == IconState.Grabbed)
		{
			Grabbed();
		}
		else if(iconState == IconState.Thrown)
		{
			Thrown();
		}

		if (Input.touchCount > 0 && iconState != IconState.Thrown)
		{
			if((Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved) && iconState == IconState.Rotating
			   && !GameObject.Find("Main Camera").GetComponent<LevelScript>().IconSelected)
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					iconState = IconState.Grabbed;
					GameObject.Find("Main Camera").GetComponent<LevelScript>().IconSelected = true;
					//startPosition = transform.position;
					//rigidbody2D.isKinematic = true;
				}
				
				
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Ended && iconState == IconState.Grabbed)
			{
				iconState = IconState.Thrown;
				GameObject.Find("Main Camera").GetComponent<LevelScript>().IconSelected = false;
				//endPosition = transform.position;
				//rigidbody2D.isKinematic = false;
			}
		}
	}

	public virtual void Rotating()
	{
		v = Quaternion.AngleAxis (degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
		transform.position = center.position + v;
	}

	public virtual void Grabbed()
	{
		startPosition = transform.position;
		Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		transform.position = pos;
	}

	public virtual void Thrown()
	{
		endPosition = transform.position;
	}
}
