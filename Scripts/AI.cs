using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AI : MonoBehaviour
{
	private GameObject Goal;
	private GameObject Player;
	private GameObject playBtn;
	public GameObject Path;
	private GameObject Clone;
	public bool Safe;
	public bool activate = false;
	private List<string> vectors = new List<string>();
	public List<string> junctions = new List<string>();
	private List<bool> paths = new List<bool>();
	private List<bool> turns = (new List<bool>{false, false, false, false});
	private float timer;
	private float rand;
	private float rand1;
	private UnityEngine.UI.Text finished;

    void Start()
    {
		Player = GameObject.Find("Player");
		Goal = GameObject.Find("Goal");
		playBtn = GameObject.Find("/Canvas/Play");
		finished = GameObject.Find("/Canvas/Finished").GetComponent<UnityEngine.UI.Text>();
		playBtn.SetActive(true);
		if ((PlayerPrefs.GetFloat("PlayerX") != 0) && (PlayerPrefs.GetFloat("PlayerZ") != 0))
		{
			Player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), 0, PlayerPrefs.GetFloat("PlayerZ"));
			Goal.transform.position = new Vector3(PlayerPrefs.GetFloat("GoalX"), 0, PlayerPrefs.GetFloat("GoalZ"));
		}
    }

	void Update()
	{
		if (activate == true)
		{
			timer += Time.deltaTime;
		}
	}

	public void Play()
	{
		playBtn.SetActive(false);
		activate = true;
		PlayerPrefs.SetFloat("PlayerX", Player.transform.position.x);
		PlayerPrefs.SetFloat("PlayerZ", Player.transform.position.z);
		PlayerPrefs.SetFloat("GoalX", Goal.transform.position.x);
		PlayerPrefs.SetFloat("GoalZ", Goal.transform.position.z);
		StartCoroutine("move");
	}

	public void Reset()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	IEnumerator move()
	{
		Clone = Instantiate(Path);
		Clone.transform.position = Player.transform.position;
		Clone.GetComponent<Renderer>().material.color = new Color(255, 0, 255);
		vectors.Add(Player.transform.localPosition.ToString());
		junctions.Add(Player.transform.localPosition.ToString());
		turns[0] = false;turns[1] = false;turns[2] = false;turns[3] = false;
		yield return new WaitForSeconds(1);
		timer = 0;
		while (true)
		{
			paths.Clear();
			for(int i = 0; i < 4; i++)
			{
				Clone = Instantiate(Player);
				Clone.transform.Translate(Vector3.forward);
				Clone.GetComponent<Renderer>().enabled = false;
				yield return new WaitForSeconds(0.025f);
				Player.transform.Rotate(0, 90, 0);
				if (vectors.Contains(Clone.transform.localPosition.ToString()))
				{
					paths.Add(false);
				}
				else
				{
					paths.Add(Safe);
				}
				if ((!Physics.Linecast(Player.transform.position, Goal.transform.position, ~ (1 << 9))) && ((Player.transform.position.x == Goal.transform.position.x) || (Player.transform.position.z == Goal.transform.position.z)))
				{
					while (Player.transform.position != Goal.transform.position)
					{
						junctions.Add(Player.transform.localPosition.ToString());
						Player.transform.position = Vector3.MoveTowards(Player.transform.position, Goal.transform.position, 1f);
						yield return new WaitForSeconds(0.1f);
					}
				}
				Destroy(Clone);
			}
			if (Player.transform.localPosition == Goal.transform.localPosition)
			{
				break;
			}
			rand = Random.Range(0, 3);
			rand1 = Random.Range(0, 2);
			if ((paths[0] == true) && (paths[1] == true) && (paths[2] == true))
			{
				junctions.Add(Player.transform.localPosition.ToString());
				if (rand == 0)
				{
					paths[0] = true;paths[1] = false;paths[2] = false;
				}
				else if (rand == 1)
				{
					paths[0] = false;paths[1] = true;paths[2] = false;
				}
				else
				{
					paths[0] = false;paths[1] = false;paths[2] = true;
				}
			}
			else if ((paths[0] == true) && (paths[1] == true) && (paths[3] == true))
			{
				junctions.Add(Player.transform.localPosition.ToString());
				if (rand == 0)
				{
					paths[0] = true;paths[1] = false;paths[3] = false;
				}
				else if (rand == 1)
				{
					paths[0] = false;paths[1] = true;paths[3] = false;
				}
				else
				{
					paths[0] = false;paths[1] = false;paths[3] = true;
				}
			}
			else if ((paths[0] == true) && (paths[2] == true) && (paths[3] == true))
			{
				junctions.Add(Player.transform.localPosition.ToString());
				if (rand == 0)
				{
					paths[0] = true;paths[2] = false;paths[3] = false;
				}
				else if (rand == 1)
				{
					paths[0] = false;paths[2] = true;paths[3] = false;
				}
				else
				{
					paths[0] = false;paths[2] = false;paths[3] = true;
				}
			}
			else if ((paths[1] == true) && (paths[2] == true) && (paths[3] == true))
			{
				junctions.Add(Player.transform.localPosition.ToString());
				if (rand == 0)
				{
					paths[1] = true;paths[2] = false;paths[3] = false;
				}
				else if (rand == 1)
				{
					paths[1] = false;paths[2] = true;paths[3] = false;
				}
				else
				{
					paths[1] = false;paths[2] = false;paths[3] = true;
				}
			}
			else if ((paths[0] == true) && (paths[1] == true))
			{
				junctions.Add(Player.transform.localPosition.ToString());
				if (rand1 == 0)
				{
					paths[0] = true;paths[1] = false;
				}
				else
				{
					paths[0] = false;paths[1] = true;
				}
			}
			else if ((paths[0] == true) && (paths[2] == true))
			{
				junctions.Add(Player.transform.localPosition.ToString());
				if (rand1 == 0)
				{
					paths[0] = true;paths[2] = false;
				}
				else
				{
					paths[0] = false;paths[2] = true;
				}
			}
			else if ((paths[0] == true) && (paths[3] == true))
			{
				junctions.Add(Player.transform.localPosition.ToString());
				if (rand1 == 0)
				{
					paths[0] = true;paths[3] = false;
				}
				else
				{
					paths[0] = false;paths[3] = true;
				}
			}
			else if ((paths[1] == true) && (paths[2] == true))
			{
				junctions.Add(Player.transform.localPosition.ToString());
				if (rand1 == 0)
				{
					paths[1] = true;paths[2] = false;
				}
				else
				{
					paths[1] = false;paths[2] = true;
				}
			}
			else if ((paths[1] == true) && (paths[3] == true))
			{
				junctions.Add(Player.transform.localPosition.ToString());
				if (rand1 == 0)
				{
					paths[1] = true;paths[3] = false;
				}
				else
				{
					paths[1] = false;paths[3] = true;
				}
			}
			else if ((paths[2] == true) && (paths[3] == true))
			{
				junctions.Add(Player.transform.localPosition.ToString());
				if (rand1 == 0)
				{
					paths[2] = true;paths[3] = false;
				}
				else
				{
					paths[2] = false;paths[3] = true;
				}
			}
			if (paths[0] == true)
			{
				if (turns[0] == false)
				{
					junctions.Add(Player.transform.localPosition.ToString());
				}
				Player.transform.Translate(Vector3.forward);
				vectors.Add(Player.transform.localPosition.ToString());
				turns[0] = true;turns[1] = false;turns[2] = false;turns[3] = false;
			}
			else if (paths[1] == true)
			{
				if (turns[1] == false)
				{
					junctions.Add(Player.transform.localPosition.ToString());
				}
				Player.transform.Translate(Vector3.right);
				vectors.Add(Player.transform.localPosition.ToString());
				turns[0] = false;turns[1] = true;turns[2] = false;turns[3] = false;
			}
			else if (paths[2] == true)
			{
				if (turns[2] == false)
				{
					junctions.Add(Player.transform.localPosition.ToString());
				}
				Player.transform.Translate(-Vector3.forward);
				vectors.Add(Player.transform.localPosition.ToString());
				turns[0] = false;turns[1] = false;turns[2] = true;turns[3] = false;
			}
			else if (paths[3] == true)
			{
				if (turns[3] == false)
				{
					junctions.Add(Player.transform.localPosition.ToString());
				}
				Player.transform.Translate(-Vector3.right);
				vectors.Add(Player.transform.localPosition.ToString());
				turns[0] = false;turns[1] = false;turns[2] = false;turns[3] = true;
			}
			else
			{
				if (junctions[junctions.Count - 1].StartsWith ("(") && junctions[junctions.Count - 1].EndsWith (")"))
				{
					junctions[junctions.Count - 1] = junctions[junctions.Count - 1].Substring(1, junctions[junctions.Count - 1].Length-2);
				}
				string[] sArray = junctions[junctions.Count - 1].Split(',');
				Vector3 result = new Vector3(float.Parse(sArray[0]), float.Parse(sArray[1]), float.Parse(sArray[2]));
				Player.transform.localPosition = result;
				paths.Clear();
				for (int i = 0; i < 4; i++)
				{
					Clone = Instantiate(Player);
					Clone.transform.Translate(Vector3.forward);
					Clone.GetComponent<Renderer>().enabled = false;
					yield return new WaitForSeconds(0.025f);
					Player.transform.Rotate(0, 90, 0);
					if (vectors.Contains(Clone.transform.localPosition.ToString()))
					{
						paths.Add(false);
					}
					else
					{
						paths.Add(Safe);
					}
					Destroy(Clone);
				}
				if ((paths[0] == false) && (paths[1] == false) && (paths[2] == false) && (paths[3] == false))
				{
					junctions.RemoveAt(junctions.Count - 1);
				}
			}
		}
		finished.enabled = true;
		if (System.Math.Round(timer, 0) == 1)
		{
			finished.text = ("Finished: " + System.Math.Round(timer, 0) + " second");
		}
		else
		{
			finished.text = ("Finished: " + System.Math.Round(timer, 0) + " seconds");
		}
		yield return new WaitForSeconds(1);
		for (int i = 0; i < junctions.Count; i++)
		{
			if (junctions[junctions.Count - 1 - i].StartsWith ("(") && junctions[junctions.Count - 1 - i].EndsWith (")"))
			{
				junctions[junctions.Count - 1 - i] = junctions[junctions.Count - 1 - i].Substring(1, junctions[junctions.Count - 1 - i].Length-2);
			}
			string[] sArray1 = junctions[junctions.Count - 1 - i].Split(',');
			Vector3 result1 = new Vector3(float.Parse(sArray1[0]), float.Parse(sArray1[1]), float.Parse(sArray1[2]));
			while (Player.transform.position != result1)
			{
				Player.transform.position = Vector3.MoveTowards(Player.transform.position, result1, 20f * Time.deltaTime);
				Clone = Instantiate(Path);
				Clone.transform.position = Player.transform.position;
				yield return new WaitForSeconds(0);
			}
		}
	}
}