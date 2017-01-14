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

    [SerializeField]
    protected GameObject errorMessage;

    [SerializeField]
    protected GameObject optionsMenu;

    [SerializeField]
    protected GameObject[] otherCanvas;

    [SerializeField]
    protected GameObject popUpInfo;


    // Use this for initialization
    void Start()
    {

    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void LoadMyCollection()
    {
        SceneManager.LoadScene("MyCollection");
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Cardgames On Motorcycles");
    }

    public void LoadOpenPacks()
    {
        SceneManager.LoadScene("OpenPacks");
    }

    public void LoadChooseDeck()
    {
        SceneManager.LoadScene("ChooseYourDeck");
    }

    public void LoadSettings()
    {
        settingsMenu.gameObject.SetActive(!settingsMenu.gameObject.active);
    }

    public void LoadSafeCheck()
    {
        safeCheck.gameObject.SetActive(!safeCheck.gameObject.active);
    }

    public void LoadErrorMessage()
    {
        errorMessage.gameObject.SetActive(!errorMessage.gameObject.active);
    }

    public void ExitAppli()
    {
        Application.Quit();
    }

    public void LoadOptions()
    {
        optionsMenu.gameObject.SetActive(!optionsMenu.gameObject.active);
    }


    public void LoadPopUpInfo()
    {
        popUpInfo.gameObject.SetActive(!popUpInfo.gameObject.active);
    }

    public void InactivateCanvases()
    {
        foreach (GameObject canvas in otherCanvas)
        {
            canvas.gameObject.SetActive(!canvas.gameObject.active);
        }
    }

}
