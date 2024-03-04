using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Ingredient
{
    Bun,
    Patty,
    Cheese,
    Bacon
}

public class DishManager : MonoBehaviour
{
    #region Singleton Code
    // A public reference to this script
    public static DishManager instance = null;

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

    private List<Dish> dishes;

    // Start is called before the first frame update
    void Start()
    {
        dishes = SetupDishes();
    }

    /// <summary>
    /// Creates all dishes that can be made
    /// </summary>
    /// <returns>A list of dishes that can be made</returns>
    private List<Dish> SetupDishes()
    {
        List<Dish> dishes = new List<Dish>();

        // Create a Dish object for each possible dish
        dishes.Add(new Dish("Hamburger", new List<Ingredient>() { Ingredient.Bun, Ingredient.Patty }));
        dishes.Add(new Dish("Cheeseburger", new List<Ingredient>() { Ingredient.Bun, Ingredient.Patty, Ingredient.Cheese }));
        dishes.Add(new Dish("Baconburger", new List<Ingredient>() { Ingredient.Bun, Ingredient.Patty, Ingredient.Cheese, Ingredient.Bacon }));

        return dishes;
    }

    /// <summary>
    /// Gets a list of all available Dishes
    /// </summary>
    /// <returns>A List of Dish objects that are available</returns>
    public List<Dish> GetAvailableDishes()
    {
        // TODO: Apply restrictions when stations for dishes have no yet been unlocked
        return dishes.FindAll(dish => dish.IsAvailable);
    }

    /// <summary>
    /// Get a dish by its name
    /// </summary>
    /// <param name="dishName">The string name of the dish</param>
    /// <returns>A Dish, could be an error dish if the given name does not make a dish</returns>
    public Dish GetDish(string dishName)
    {
        foreach(Dish dish in dishes)
        {
            if(dish.Name.ToLower() == dishName.ToLower())
			{
				if(!dish.IsAvailable)
                {
                    Debug.Log("Warning! This dish exists but is not available!");
                    return null;
                }
                return dish;
			}
        }

        Debug.Log("Error! No dish found with that name.");
        return null;
    }

    /// <summary>
    /// Get a dish by its ingredients
    /// </summary>
    /// <param name="ingredients">The list of ingredients</param>
    /// <returns>A Dish, could be an error dish if the given ingredients do not make a dish</returns>
    public Dish GetDish(List<Ingredient> ingredients)
    {
        ingredients.Sort();
        foreach(Dish dish in dishes)
		{
            dish.Ingredients.Sort();
            if(AreListsEqual(dish.Ingredients, ingredients))
			{
                if(!dish.IsAvailable)
                {
                    Debug.Log("Warning! This dish exists but is not available!");
                    return null;
                }
                return dish;
			}
        }

        Debug.Log("Error! No dish found with those ingredients.");
        return null;
    }

    /// <summary>
    /// Compares two lists of items
    /// </summary>
    /// <typeparam name="T">The type of each list</typeparam>
    /// <param name="list1">The first list</param>
    /// <param name="list2">The second list</param>
    /// <returns>Whether the lists are equal</returns>
    public bool AreListsEqual<T>(List<T> list1, List<T> list2)
	{
        var firstNotSecond = list1.Except(list2).ToList();
        var secondNotFirst = list2.Except(list1).ToList();

        return !firstNotSecond.Any() && !secondNotFirst.Any();
    }

    /// <summary>
    /// Get a random dish from the available dishes
    /// </summary>
    /// <returns>A random available dish</returns>
    public Dish GetRandomDish()
	{
        // Get the array of available dish and its length 
        List<Dish> dishes = GetAvailableDishes();
        int dishesCount = dishes.Count;

        // Get a random dish
        int randomDishNum = Random.Range(0, dishesCount);
        return dishes[randomDishNum];
    }
}
