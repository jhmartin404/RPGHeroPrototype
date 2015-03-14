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
	Stopped,
	Rotating,
	Grabbed,
	Thrown
};

public class Icon : MonoBehaviour
{
	protected const float fingerRadius = 0.5f;
	protected GameObject mainCamera;
	protected IconType iconType;
	protected GameObject slot = null;
	protected IconState iconState;
	protected float iconSpeed = 5.0f;

	protected Vector2 startPosition;//start position
	protected Vector2 endPosition;//end position

	public GameObject Slot
	{
		get
		{
			return slot;
		}
		set
		{
			slot = value;
		}
	}

	public IconType Type
	{
		get
		{
			return iconType;
		}
		set
		{
			iconType = value;
		}
	}

	public IconState State
	{
		get
		{
			return iconState;
		}
		set
		{
			iconState = value;
		}
	}

	// Use this for initialization
	public virtual void Start () 
	{
		mainCamera = GameObject.Find ("Main Camera");
		iconState = IconState.Rotating;
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
		if(iconState != IconState.Stopped)
		{
			if (Input.touchCount > 0 && iconState != IconState.Thrown)
			{
				if((Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved) && iconState == IconState.Rotating
			 	  && !mainCamera.GetComponent<LevelScript>().IconSelected && OnCheckSelected())
				{
					Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
					if (GetComponent<Collider2D>() == Physics2D.OverlapCircle(touchPos, fingerRadius))
					{
						OnIconTouched();
					}				
				}
				else if(Input.GetTouch(0).phase == TouchPhase.Ended && iconState == IconState.Grabbed)
				{
					OnIconLetGo();
				}
			}

			if(iconState == IconState.Rotating)
			{
				OnRotatingState();
			}
			else if(iconState == IconState.Grabbed)
			{
				OnGrabbedState();
			}
			else if(iconState == IconState.Thrown)
			{
				OnThrownState();
			}
		}
	}

	protected virtual bool OnCheckSelected()
	{
		return true;
	}

	protected virtual void OnIconTouched()
	{
		slot = null;
		iconState = IconState.Grabbed;
		mainCamera.GetComponent<LevelScript>().IconSelected = true;
		GetComponent<Rigidbody2D>().isKinematic = true;
	}

	protected virtual void OnIconLetGo()
	{
		iconState = IconState.Thrown;
		mainCamera.GetComponent<LevelScript>().IconSelected = false;
		GetComponent<Rigidbody2D>().isKinematic = false;	
	}

	public virtual void LateUpdate()
	{
		if(iconState == IconState.Thrown)
		{
			GetComponent<Rigidbody2D>().velocity = (endPosition - startPosition).normalized*iconSpeed;
		}
	}

	protected virtual void OnRotatingState()
	{
		if(slot != null)
		{
			transform.position = slot.transform.position;
		}
	}

	protected virtual void OnGrabbedState()
	{
		startPosition = transform.position;
		Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		transform.position = pos;
	}

	protected virtual void OnThrownState()
	{
		endPosition = transform.position;
	}

	public void StopIcon()
	{
		iconState = IconState.Stopped;
	}

	public void RestartIcon()
	{
		iconState = IconState.Rotating;
	}

	protected virtual void OnTriggerStay2D(Collider2D other)
	{
		OnHit (other);
	}

	protected virtual void OnHit(Collider2D col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			OnHitEnemy(col);
		}
		else if(col.gameObject.tag == "Player")
		{
			OnHitPlayer(col);
		}
	}

	protected virtual void OnHitEnemy(Collider2D col)
	{

	}
	
	protected virtual void OnHitPlayer(Collider2D col)
	{

	}

	public virtual void OnDestroy()
	{
		Destroy (gameObject);
	}

	void OnBecameInvisible()
	{
		OnDestroy ();
	}
}
