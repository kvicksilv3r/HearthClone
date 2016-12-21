using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//By Johanna Pettersson

public class ButtonBehaviour : MonoBehaviour {

    [SerializeField]
    protected GameObject settingsMenu;
    [SerializeField]
    protected GameObject safeCheck;

    // Use this for initialization
    void Start () {
		
	}
	
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Cardgames On Motorcycles");
    }

    public void LoadSettings()
    {
        settingsMenu.gameObject.SetActive(!settingsMenu.gameObject.active);
    }

    public void LoadSafeCheck()
    {
        safeCheck.gameObject.SetActive(!safeCheck.gameObject.active);
    }

}
