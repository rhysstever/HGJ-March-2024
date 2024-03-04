using System;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField]
    private GameObject ordersText, plateText;

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

    /// <summary>
    /// Initial logic when the menu state changes
    /// </summary>
    /// <param name="newMenuState">The new menu state</param>
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
                UpdateRemainingOrdersText(OrdersManager.instance.GetRemainingOrdersText());
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

    /// <summary>
    /// Update the text for the orders list
    /// </summary>
    /// <param name="remainingOrdersText">The new text of the orders list</param>
    public void UpdateRemainingOrdersText(string remainingOrdersText)
	{
        ordersText.GetComponent<TMP_Text>().text = remainingOrdersText;
    }

    /// <summary>
    /// Update the list of plated ingredients
    /// </summary>
    /// <param name="ingredients">The List of ingredients prepped and plated</param>
    public void UpdatePlateText(List<Ingredient> ingredients)
    {
        string currentPlateText = "Current Plate:";
        ingredients.ForEach(ingredient => currentPlateText += $"\n{ingredient.ToString()}");
        plateText.GetComponent<TMP_Text>().text = currentPlateText;
    }
}
