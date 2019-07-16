using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : MonoBehaviour
{
	private GameObject Goal;
	private bool collide;

	void Start()
	{
		Goal = GameObject.Find("Goal");
		collide = false;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.root.name == "Walls")
		{
			collide = true;
		}
	}

	void Update()
	{
		if (collide == true)
		{
			Goal.GetComponent<AI>().Safe = false;
		}
		else
		{
			Goal.GetComponent<AI>().Safe = true;
		}

	}
}
