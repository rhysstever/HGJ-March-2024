using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish
{
    private string name;
    private List<Ingredient> ingredients;
    private bool isAvailable;

    public string Name { get { return name; } }
    public List<Ingredient> Ingredients { get { return ingredients; } }
    public bool IsAvailable { get { return isAvailable; } }

    public Dish(string name, List<Ingredient> ingredients)
    {
        this.name = name;
        this.ingredients = ingredients;
        this.ingredients.Sort();
        isAvailable = true;
    }

    public Dish(string name, List<Ingredient> ingredients, bool isAvailable)
	{
        this.name = name;
        this.ingredients = ingredients;
        this.ingredients.Sort();
        this.isAvailable = isAvailable;
    }

    public override string ToString()
	{
        string ingredientsText = ingredients[0].ToString();
        for(int i = 1; i < ingredients.Count; i++)
		{
            ingredientsText += $" and {ingredients[i].ToString()}";
        }

        string availableText = "available";
        if(!isAvailable) availableText = $"not {availableText}";

        return $"{name} requires {ingredientsText}. It is {availableText}.";
    }
}
