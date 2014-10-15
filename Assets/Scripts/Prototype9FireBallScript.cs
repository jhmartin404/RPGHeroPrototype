using UnityEngine;
using System.Collections;

public class Prototype9FireBallScript : MonoBehaviour 
{

	public Transform center;
	public float degreesPerSecond = 85.0f;
	private bool isGrabbed = false;
	private bool isTouched = false;
	private bool isThrown = false;
	private float speed = 5.0f;
	private Vector2 startPosition;
	private Vector2 endPosition;
	private float lifeTime = 4;
	private float timeElapsed;
	private bool halfLife = false;
	private float fingerRadius = 0.5f;

	private Vector3 v;

	// Use this for initialization
	void Start () 
	{
		v = transform.position - center.position;
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (Input.touchCount > 0 && !isThrown)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetTouch(0).phase == TouchPhase.Moved && !isTouched && !Prototype9Layout.getIconSelected()))
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					isGrabbed = true;		
					//startPosition = transform.position;
					rigidbody2D.isKinematic = true;
					Prototype9Layout.setIconSelected(true);
				}


			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && isGrabbed) 
			{
				startPosition = transform.position;
				Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				transform.position = pos;
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Ended && isGrabbed)
			{
				if(!isTouched)
					Destroy(gameObject);
				if(isTouched)
				{
					isThrown = true;
					isTouched = false;
				}
				endPosition = transform.position;
				rigidbody2D.isKinematic = false;
				Prototype9Layout.setIconSelected(false);
			}
		}

		if (!isTouched && !isThrown && !isGrabbed) 
		{
			v = Quaternion.AngleAxis (degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
			transform.position = center.position + v;

			timeElapsed += Time.deltaTime;
			if(timeElapsed >= lifeTime/2 && !halfLife)
			{
				Color originalColor = renderer.material.color;
				renderer.material.color = new Color (originalColor.r, originalColor.g, originalColor.b, 0.5f);
				halfLife = true;
			}
			
			if(timeElapsed >= lifeTime && (!isTouched || !isThrown))
			{
				Destroy(gameObject);
			}
		}

//		timeElapsed += Time.deltaTime;
//		if(timeElapsed >= lifeTime/2 && !halfLife)
//		{
//			Color originalColor = renderer.material.color;
//			renderer.material.color = new Color (originalColor.r, originalColor.g, originalColor.b, 0.5f);
//			halfLife = true;
//		}
//		
//		if(timeElapsed >= lifeTime && (!isTouched || !isThrown))
//		{
//			Destroy(gameObject);
//		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Color collisionColor = new Color (0, 0, 0, 255);
		if(other.gameObject.tag == "ActionArea")
		{
			isTouched=true;
			other.renderer.material.color = collisionColor;
		}
	}

	void LateUpdate()
	{
		if(isThrown)
		{
			rigidbody2D.velocity = (endPosition - startPosition).normalized*speed;
		}
	}

	public bool getIsThrown()
	{
		return isThrown;
	}

	public bool getIsTouched()
	{
		return isTouched;
	}
}
