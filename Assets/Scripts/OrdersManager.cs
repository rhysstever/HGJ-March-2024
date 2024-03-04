using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersManager : MonoBehaviour
{
	#region Singleton Code
	// A public reference to this script
	public static OrdersManager instance = null;

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

	private DishManager dishManager;
	private List<Dish> orders;
	private List<Ingredient> currentPlating;

	// Start is called before the first frame update
	void Start()
	{
		dishManager = DishManager.instance;
		orders = new List<Dish>();
		currentPlating = new List<Ingredient>();

		// Add 5 random orders to start
		while(orders.Count < 5)
		{
			AddRandomOrder();
		}
	}

	// Update is called once per frame
	void Update()
	{
		// Press SPACE to submit the current plate
		if(Input.GetKeyDown(KeyCode.Space))
		{
			SubmitPlate();
		}
	}

	/// <summary>
	/// Add a random order to the order list
	/// </summary>
	public void AddRandomOrder()
	{
		AddOrder(dishManager.GetRandomDish());
	}

	/// <summary>
	/// Add an order to the order list
	/// </summary>
	/// <param name="newDish">The Dish to be added to the order list</param>
	public void AddOrder(Dish newDish)
	{
		orders.Add(newDish);
	}

	/// <summary>
	/// Creates a String describing the remaining orders
	/// </summary>
	/// <returns>A String about the remaining orders</returns>
	public string GetRemainingOrdersText()
	{
		string remainingOrdersText = $"Remaining Orders:";

		for(int i = 0; i < orders.Count; i++)
			remainingOrdersText += $"\n{orders[i].Name}";

		return remainingOrdersText;
	}

	/// <summary>
	/// Clear the current plate
	/// </summary>
	public void ClearPlating()
	{
		currentPlating.Clear();
		UIManager.instance.UpdatePlateText(currentPlating);
	}

	/// <summary>
	/// Add an ingredient to the plate
	/// </summary>
	/// <param name="ingredient">The ingredient that is being added to the plate</param>
	public void AddToPlate(Ingredient ingredient)
	{
		currentPlating.Add(ingredient);
		UIManager.instance.UpdatePlateText(currentPlating);
	}

	/// <summary>
	/// Submits a plate to the order list
	/// </summary>
	private void SubmitPlate()
	{
		Dish plateDish = dishManager.GetDish(currentPlating);

		if(plateDish == null)
		{
			// Return early if the plated dish does not match 
			Debug.Log("Error! Not a complete dish");
			return;
		} 
		else
		{
			int orderIndex = FindNextOrderOf(plateDish);
			if(orderIndex == -1)
			{
				// Return early if the dish is not in the order list 
				Debug.Log("Error! The dish is not an order");
				return;
			}
			orders.RemoveAt(orderIndex);
			Debug.Log($"{plateDish.Name} completed. Order removed.");

			// Update UI
			string remainingOrdersText = GetRemainingOrdersText();
			UIManager.instance.UpdateRemainingOrdersText(remainingOrdersText);
		}

		ClearPlating(); // Clear the plate after the dish is submitted
	}

	/// <summary>
	/// Finds the first index of a Dish in the order list
	/// </summary>
	/// <param name="dish">A Dish object</param>
	/// <returns>The index of the Dish in the order list. Returns -1 if not in list</returns>
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
