using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour, NSFSM.IContext {

    public static Game instance = null;
    public NSFSM.FSM fsm;

    public NSGame.Resources resources;
    public NSGame.Settings settings;

    public void Start()
    {
        // Prevent multiple game instance
        if (!instance)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        
        fsm.Launch(this);
    }

    public void Update()
    {
        fsm.Tick();
    }



}
