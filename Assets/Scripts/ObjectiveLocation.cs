using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveLocation : MonoBehaviour
{
    #region Singleton
    private static ObjectiveLocation instance;


    private void Awake()
    {
        instance = this;
    }

    public static ObjectiveLocation Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new ObjectiveLocation();
            }

            return instance;
        }
    }
    #endregion

    public GameObject objective;
}
