using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTargeting : MonoBehaviour
{

	public GameObject lastTarget;
	public RaycastHit hit;
	RaycastHit hit2;
	Ray ray;
	Ray ray2;
	public LayerMask lMask;
	public LayerMask lMask2;

	// Use this for initialization
	void Start()
	{
		ray = new Ray(transform.position, Vector3.forward);
		ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
	}

	// Update is called once per frame
	void Update()
	{
	}

	public void OnMouseDrag()
	{
		ray.origin = transform.position;
		Physics.Raycast(ray, out hit, 1000, lMask);
		ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
		Physics.Raycast(ray2, out hit2, 1000, lMask2);
		transform.position = hit2.point + new Vector3(0, 1, 0);
		Debug.DrawRay(transform.position, Vector3.forward * 1000, Color.red);
	}

	public void OnMouseUp()
	{
		if (hit.transform)
		{
			if (hit.transform.gameObject.CompareTag("Card"))
			{
				if (hit.transform != transform.parent.parent)
				{
					//if (transform.parent.parent.GetComponent<CardClass>().OwnerId != hit.transform.GetComponent<CardClass>().OwnerId)
					{
						lastTarget = hit.transform.gameObject;
						print("Target hit was: " + lastTarget.GetComponent<CardClass>().CardName);

                        //if(Draggable.spellCard && lastTarget == hit.transform.gameObject)
                        //{
                        //    Destroy();
                        //}
					}
				}
			}
		}

		transform.position = transform.parent.position;
	}
}
