using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    static public GameManager GetInstance()
    {
        return instance;
    }
    private void Update()
    {
   

    }

    static public bool GetAxisOnce(ref bool _pressedAlready, string _name)
    {
        bool current = Input.GetAxisRaw(_name) > 0;
        if (current && _pressedAlready)
        {
            return false;
        }

        _pressedAlready = current;

        return current;
    }
}
