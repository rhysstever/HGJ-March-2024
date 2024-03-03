using System.Collections;
using System.Collections.Generic;
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

    public List<Dish> dishes;

    // Start is called before the first frame update
    void Start()
    {
        dishes = SetupDishes();

        Debug.Log("Dishes: ");
        dishes.ForEach(dish => Debug.Log(dish.ToString()));
    }

    private List<Dish> SetupDishes()
    {
        List<Dish> dishes = new List<Dish>();

        dishes.Add(new Dish("Hamburger", new List<Ingredient>() { Ingredient.Bun, Ingredient.Patty }));
        dishes.Add(new Dish("Cheeseburger", new List<Ingredient>() { Ingredient.Bun, Ingredient.Patty, Ingredient.Cheese }));
        dishes.Add(new Dish("Baconburger", new List<Ingredient>() { Ingredient.Bun, Ingredient.Patty, Ingredient.Cheese, Ingredient.Bacon }));

        return dishes;
    }

    public Dish GetDish(string dishName)
    {
        List<Dish> sameNameDishes = dishes.FindAll(dish => dish.Name.ToLower() == dishName.ToLower());
        if(sameNameDishes.Count > 0)
            return sameNameDishes[0];
        else
            return new Dish("Error Dish", new List<Ingredient>());
    }

    public Dish GetDish(List<Ingredient> ingredients)
    {
        ingredients.Sort();
        List<Dish> sameNameDishes = dishes.FindAll(dish => dish.Ingredients == ingredients);
        if(sameNameDishes.Count > 0)
            return sameNameDishes[0];
        else
            return new Dish("Error Dish", new List<Ingredient>());
    }

    public List<Dish> GetAvailableDishes()
    {
        // TODO: Apply restrictions when stations for dishes have no yet been unlocked
        return dishes.FindAll(dish => dish.IsAvailable);
    }

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
