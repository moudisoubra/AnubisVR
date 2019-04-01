using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class HeapHajjo<T> where T : IheapItem<T>
{
    T[] items;
    int currentItemsCount;


    public HeapHajjo(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    public void Add(T item)
    {
        item.HeapIndex = currentItemsCount;
        items[currentItemsCount] = item;
        SortUp(item);
        currentItemsCount++;
    }


    public T RemoveFirst()
    {
        T firtItem = items[0];
        currentItemsCount--;
        items[0] = items[currentItemsCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firtItem;
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }


    public int Count
    {
        get
        {
            return currentItemsCount;
        }
    }

    public bool contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }

    void SortDown(T item)
    {
        while (true)
        {
            int childIndextLeft = item.HeapIndex * 2 + 1;
            int childIndextRight = item.HeapIndex * 2 + 1;
            int SwapIndex = 0;

            if (childIndextLeft < currentItemsCount)
            {
                SwapIndex = childIndextLeft;


                if (childIndextRight < currentItemsCount)
                {
                    if (items[childIndextLeft].CompareTo(items[childIndextRight]) < 0)
                    {
                        SwapIndex = childIndextRight;
                    }

                    if (item.CompareTo(items[SwapIndex]) < 0)
                    {
                        Swap(item, items[SwapIndex]);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else { return; }


        }
    }

    void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;

        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
            {
                Swap(item, parentItem);
            }
            else
            {
                break;
            }

            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    void Swap(T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        int itemIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemIndex;
    }

}



public interface IheapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;

        set;
    }
}
