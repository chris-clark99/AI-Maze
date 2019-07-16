using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour
{
	private RaycastHit hit;
	private Ray ray;
	private GameObject selected;
	private bool active;

    void Update()
    {
		active = GameObject.Find("Goal").GetComponent<AI>().activate;
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if ((Input.GetMouseButtonDown (0)) && (active == false))
		{
			if (Physics.Raycast (ray, out hit, 100.0f))
			{
				if(hit.collider.name == "GoalCol")
				{
					selected = GameObject.Find("Goal");
				}
				if(hit.collider.name == "PlayerCol")
				{
					selected = GameObject.Find("Player");
				}
			}
		}
		if (Input.GetMouseButtonUp (0))
		{
			selected = null;
		}
		if ((Physics.Raycast (ray, out hit, 100.0f, ~ (1 << 9))) && (active == false))
		{
			if((hit.collider.name == "Plane") && (selected != null))
			{
				selected.transform.position = new Vector3(Mathf.RoundToInt(hit.point.x), Mathf.RoundToInt(hit.point.y), Mathf.RoundToInt(hit.point.z));
			}
		}
    }
}
