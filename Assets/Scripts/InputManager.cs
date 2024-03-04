using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Dictionary<KeyCode, Ingredient> ingredientInputMap; 

    // Start is called before the first frame update
    void Start()
    {
        ingredientInputMap = SetupIngredientInputs();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Swap this with the new input system
        if(Input.GetKeyDown(KeyCode.Escape))
            OrdersManager.instance.ClearPlating();

        if(Input.GetKeyDown(KeyCode.B))
            AddIngredientFromInput(KeyCode.B);
        else if(Input.GetKeyDown(KeyCode.P))
            AddIngredientFromInput(KeyCode.P);
        else if(Input.GetKeyDown(KeyCode.C))
            AddIngredientFromInput(KeyCode.C);
        else if(Input.GetKeyDown(KeyCode.N))
            AddIngredientFromInput(KeyCode.N);
    }

    // TODO: Swap this with the new input system
    private Dictionary<KeyCode, Ingredient> SetupIngredientInputs()
	{
        Dictionary<KeyCode, Ingredient> ingredientInputMap = new Dictionary<KeyCode,Ingredient>();

        ingredientInputMap.Add(KeyCode.B, Ingredient.Bun);
        ingredientInputMap.Add(KeyCode.P, Ingredient.Patty);
        ingredientInputMap.Add(KeyCode.C, Ingredient.Cheese);
        ingredientInputMap.Add(KeyCode.N, Ingredient.Bacon);

        return ingredientInputMap;
    }

    private void AddIngredientFromInput(KeyCode keyInput)
	{
        if(ingredientInputMap.ContainsKey(keyInput))
            OrdersManager.instance.AddToPlate(ingredientInputMap[keyInput]);
	}
}
