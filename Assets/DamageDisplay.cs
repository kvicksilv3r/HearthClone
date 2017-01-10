using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDisplay : MonoBehaviour {

	public float timeToDie;

	// Use this for initialization
	void Start()
	{
		StartCoroutine("KillSelf");
	}

	public void SetText(int damage)
	{
		transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
	}

	// Update is called once per frame
	IEnumerator KillSelf()
	{
		yield return new WaitForSeconds(timeToDie);
		Destroy(gameObject);
	}
}
