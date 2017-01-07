using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMuligan : MonoBehaviour {

	void OnMouseDown()
	{
		GameObject.Find("GameManager").GetComponent<GameManager>().EndMuligan();
	}
}
