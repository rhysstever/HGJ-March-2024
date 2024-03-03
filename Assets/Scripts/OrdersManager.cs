using System;
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

public class OrdersManager : MonoBehaviour
{
	public List<Dish> dishes;
	public List<Dish> orders;

	// Start is called before the first frame update
	void Start()
	{
		dishes = SetupDishes();
		orders = new List<Dish>();

		while(orders.Count < 3)
		{
			//AddOrder(GetDish("Hamburger"));
			AddRandomOrder();
		}

		Debug.Log("Dishes: ");
		dishes.ForEach(dish => Debug.Log(dish.ToString()));
		Debug.Log("Orders: ");
		orders.ForEach(order => Debug.Log(order.Name.ToString()));
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Q))
			FinishOrder(GetDish("Hamburger"));
		if(Input.GetKeyDown(KeyCode.W))
			FinishOrder(GetDish("Cheeseburger"));
		if(Input.GetKeyDown(KeyCode.E))
			FinishOrder(GetDish("Baconburger"));
	}

	private List<Dish> SetupDishes()
	{
		List<Dish> dishes = new List<Dish>();

		dishes.Add(new Dish("Hamburger", new List<Ingredient>() { Ingredient.Bun, Ingredient.Patty }));
		dishes.Add(new Dish("Cheeseburger", new List<Ingredient>() { Ingredient.Bun, Ingredient.Patty, Ingredient.Cheese }));
		dishes.Add(new Dish("Baconburger", new List<Ingredient>() { Ingredient.Bun, Ingredient.Patty, Ingredient.Cheese, Ingredient.Bacon }));

		return dishes;
	}

	public List<Dish> GetAvailableDishes()
	{
		// TODO: Apply restrictions when stations for dishes have no yet been unlocked
		return dishes.FindAll(dish => dish.IsAvailable);
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

	public void AddRandomOrder()
	{
		// Get the array of available dish and its length 
		List<Dish> dishes = GetAvailableDishes();
		int dishesCount = dishes.Count;

		// Get a random dish
		int randomDishNum = UnityEngine.Random.Range(0, dishesCount);
		Dish randomDish = dishes[randomDishNum];

		// Add the dish to the queue
		AddOrder(randomDish);
	}

	public void AddOrder(Dish newDish)
	{
		orders.Add(newDish);
	}

	public void FinishOrder(Dish completedDish)
	{
		int orderIndex = FindNextOrderOf(completedDish);
		if(orderIndex == -1)
		{
			Debug.Log("Error! Completed dish was not an order");
			return;
		}
		orders.RemoveAt(orderIndex);
		Debug.Log($"{completedDish} completed. Order removed.");

		string remainingOrderNames = string.Empty;
		orders.ForEach(order => remainingOrderNames += $" | {order.Name}");
		Debug.Log($"{remainingOrderNames} remain");
	}

	private int FindNextOrderOf(Dish dish)
	{
		for(int i = 0; i < orders.Count; i++)
		{
			if(dish == orders[i])
				return i;
		}

		return -1;
	}
}
