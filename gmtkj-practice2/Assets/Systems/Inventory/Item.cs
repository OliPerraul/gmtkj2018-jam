using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory Item")]
public class Item : ScriptableObject
{

    public Sprite icon;
    public int count;
    public float cost;

    public bool Add(ref Item a, ref Item b)
    {
        if (a.Same(b))
        {
            a.count += b.count;
            return true;

        }

        return false;

    }

    public bool Same(Item item)
    {
        return name == item.name;
    }
}
