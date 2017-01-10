using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDisplay : MonoBehaviour {

	protected float timeToDie = 1;

	public void SetText(int damage)
	{
		transform.GetChild(0).GetComponent<TextMesh>().text = "-" + damage.ToString();
		transform.position = transform.parent.position + new Vector3(0, 3.5f, -1);
		StartCoroutine("KillSelf");
	}

	// Update is called once per frame
	IEnumerator KillSelf()
	{
		yield return new WaitForSeconds(timeToDie);
		Destroy(gameObject);
	}
}
