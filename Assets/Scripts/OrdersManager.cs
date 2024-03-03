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
	public List<Dish> orders;

	// Start is called before the first frame update
	void Start()
	{
		dishManager = DishManager.instance;
		orders = new List<Dish>();

		while(orders.Count < 3)
		{
			//AddOrder(GetDish("Hamburger"));
			AddRandomOrder();
		}

		Debug.Log("Orders: ");
		orders.ForEach(order => Debug.Log(order.Name.ToString()));
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Q))
			FinishOrder(dishManager.GetDish("Hamburger"));
		if(Input.GetKeyDown(KeyCode.W))
			FinishOrder(dishManager.GetDish("Cheeseburger"));
		if(Input.GetKeyDown(KeyCode.E))
			FinishOrder(dishManager.GetDish("Baconburger"));
	}

	public void AddRandomOrder()
	{
		AddOrder(dishManager.GetRandomDish());
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
		Debug.Log($"{completedDish.Name} completed. Order removed.");

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
