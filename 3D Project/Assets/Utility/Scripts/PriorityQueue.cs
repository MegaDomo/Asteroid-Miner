using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T>
{

	List<Tuple<T, double>> elements = new List<Tuple<T, double>>();


	/// <summary>
	/// Return the total number of elements currently in the Queue.
	/// </summary>
	/// <returns>Total number of elements currently in Queue</returns>
	public int Count
	{
		get { return elements.Count; }
	}


	/// <summary>
	/// Add given item to Queue and assign item the given priority value.
	/// </summary>
	/// <param name="item">Item to be added.</param>
	/// <param name="priorityValue">Item priority value as Double.</param>
	public void Enqueue(T item, double priorityValue)
	{
		elements.Add(Tuple.Create(item, priorityValue));
	}


	/// <summary>
	/// Return lowest priority value item and remove item from Queue.
	/// </summary>
	/// <returns>Queue item with lowest priority value.</returns>
	public T Dequeue()
	{
		int bestPriorityIndex = 0;

		for (int i = 0; i < elements.Count; i++)
		{
			if (elements[i].Item2 < elements[bestPriorityIndex].Item2)
			{
				bestPriorityIndex = i;
			}
		}

		T bestItem = elements[bestPriorityIndex].Item1;
		elements.RemoveAt(bestPriorityIndex);
		return bestItem;
	}


	/// <summary>
	/// Return lowest priority value item without removing item from Queue.
	/// </summary>
	/// <returns>Queue item with lowest priority value.</returns>
	public T Peek()
	{
		int bestPriorityIndex = 0;

		for (int i = 0; i < elements.Count; i++)
		{
			if (elements[i].Item2 < elements[bestPriorityIndex].Item2)
			{
				bestPriorityIndex = i;
			}
		}

		T bestItem = elements[bestPriorityIndex].Item1;
		return bestItem;
	}
}
