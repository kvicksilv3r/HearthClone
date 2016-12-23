using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
/* HOW IT WORKS:
 * Use loadFile("file_name.txt",000); method by choosing file_name and card ID [this can be found temporary in the Update() method]
 * In ParseJson() you can get the variables value by writing CardStats.variable.[Example:debug.log(CardStats.health) will give the output: 15]
 */
public class ParseFromJSON : MonoBehaviour
{
	string loadJson;

	// Use this for initialization
	void Start()
	{

	}
	CARDS ParseJson(string selectedJSON) //string array from StringStorage players card deck
	{
		CARDS CardStats = new CARDS();
		JsonUtility.FromJsonOverwrite(selectedJSON, CardStats); //parse the jsontable to values for the variables in class CARDS
		return CardStats;
	}

	public CARDS loadFile(int ID)
	{
		using (StreamReader sr = new StreamReader(System.IO.Path.Combine(Application.streamingAssetsPath, "StreamingAssets/deckLib.txt")))
		{
			List<string> jsonlist = new List<string>();
			while ((loadJson = sr.ReadLine()) != null)
			{
				if (loadJson.Contains("\"card_id\":" + ID.ToString())) // letar efter table i txt-filen
				{
					Debug.Log("Load table:" + loadJson);
					return ParseJson(loadJson);
				}
			}
		}
		return null;
	}
	void SendValues()
	{
		//Send CARDS variable values to the file where they rend cards.
	}
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.C)) //exempel input
		{
			//loadFile("Test_json.txt", 15);  //exempel värden

		}
	}

}