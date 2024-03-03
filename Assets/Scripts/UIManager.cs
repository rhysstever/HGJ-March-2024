using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuState
{
    MainMenu,
    Controls,
    Game,
    Pause,
    GameEnd
}

public class UIManager : MonoBehaviour
{
    #region Singleton Code
    // A public reference to this script
    public static UIManager instance = null;

    // Awake is called even before start 
    // (I think its at the very beginning of runtime)
    private void Awake()
    {
        // If the reference for this script is null, assign it this script
        if(instance == null)
            instance = this;
        // If the reference is to something else (it already exists)
        // than this is not needed, thus destroy it
        else if(instance != this)
            Destroy(gameObject);
    }
    #endregion

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private GameObject mainMenuUIParent, controlsUIParent, gameUIParent, pauseUIParent, gameEndUIParent;

    private MenuState currentMenuState;

    public MenuState CurrentMenuState { get { return currentMenuState; } }

    // Start is called before the first frame update
    void Start()
    {
        ChangeMenuState(currentMenuState);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
		{
            int newMenuStateIndex = (int)currentMenuState + 1;
            if(newMenuStateIndex >= Enum.GetValues(typeof(MenuState)).Length)
                newMenuStateIndex = 0;
            MenuState newMenuState = (MenuState)newMenuStateIndex;
            
            ChangeMenuState(newMenuState);
		}
    }

    public void ChangeMenuState(MenuState newMenuState)
	{
        // Deactivate all other parent UIs
        for(int i = 0; i < canvas.transform.childCount; i++)
            canvas.transform.GetChild(i).gameObject.SetActive(false);

        switch(currentMenuState)
        {
            case MenuState.MainMenu:
                mainMenuUIParent.SetActive(true);
                break;
            case MenuState.Controls:
                controlsUIParent.SetActive(true);
                break;
            case MenuState.Game:
                gameUIParent.SetActive(true);
                break;
            case MenuState.Pause:
                pauseUIParent.SetActive(true);
                break;
            case MenuState.GameEnd:
                gameEndUIParent.SetActive(true);
                break;
        }

        currentMenuState = newMenuState;
    }
}
