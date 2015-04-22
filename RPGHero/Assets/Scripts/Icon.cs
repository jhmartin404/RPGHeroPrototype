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
	protected const float fingerRadius = 0.4f;
	protected GameObject mainCamera;
	protected IconType iconType;
	protected GameObject slot = null;
	protected IconState iconState;
	protected float iconSpeed = 5.0f;

	protected Vector2 startPosition;//start position
	protected Vector2 prevPostiton;
	protected Vector2 endPosition;//end position
	private Renderer iconRenderer;

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
		iconRenderer = GetComponent<Renderer> ();
		//normalColor = iconRenderer.material.color;
		//disabledColor = new Color (90, 75, 75, 255);
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
		if(iconState != IconState.Stopped)
		{
			if(!OnCheckSelected())
			{
				iconRenderer.material.SetColor("_Color",Color.gray);
			}
			else if(OnCheckSelected())
			{
				if(iconRenderer.material.GetColor("_Color") ==  Color.gray)
				{
					iconRenderer.material.SetColor("_Color",Color.white);
				}
			}
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
			if(endPosition-startPosition == Vector2.zero)
			{
				Debug.LogWarning("ZERO VECTOR");
				startPosition.y -=1;
			}
			GetComponent<Rigidbody2D>().velocity = (endPosition - startPosition).normalized*iconSpeed;
		}
	}

	protected virtual void OnRotatingState()
	{
		if(slot != null)
		{
			Vector3 iconPos = slot.transform.position;
			iconPos.z -= 0.1f;
			transform.position = iconPos;
		}
	}

	protected virtual void OnGrabbedState()
	{
		Vector2 tempPostion = transform.position;
		Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		if(tempPostion-pos != Vector2.zero)
		{
			startPosition = tempPostion;
			transform.position = pos;
		}
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

	public virtual void OnDestroy()
	{
		Destroy (gameObject);
	}

	void OnBecameInvisible()
	{
		OnDestroy ();
	}
}
