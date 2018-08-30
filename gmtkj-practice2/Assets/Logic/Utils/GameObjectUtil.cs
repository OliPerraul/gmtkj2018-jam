using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GameObjectUtil
{
    public static void DestroyChildren(GameObject o)
    {
        foreach (Transform child in o.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static void DestroyImmediateChildren(GameObject o)
    {
        foreach (Transform child in o.transform)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }
    }


}

