using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    private FSM<SceneController> _fsm;

    private SceneInt _sI;

    public RotationController myRC;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _fsm = new FSM<SceneController>(this);
        _fsm.TransitionTo<State_Standby>();

        Debug.Assert(myRC != null, "Missing Rotation Controller Reference");

        SceneManager.LoadScene(_sI.LoadNext(), LoadSceneMode.Additive);

    }

    private void Update()
    {
        _fsm.Update();
    }


    #region States
    private class State_Base : FSM<SceneController>.State
    {

    }

    private class State_Standby : State_Base
    {
        public override void Update()
        {
            base.Update();
            if (Context.myRC.button_state)
                TransitionTo<State_LoadNextScene>();
        }
    }

    private class State_LoadNextScene : State_Base
    {
        private int ToLoad;

        public override void OnEnter()
        {
            base.OnEnter();
            SceneManager.UnloadScene(Context._sI.GetCurrent());
            ToLoad = Context._sI.LoadNext();
        }

        public override void Update()
        {
            base.Update();
            SceneManager.LoadScene(ToLoad, LoadSceneMode.Additive);
            TransitionTo<State_Standby>();
        }
    }

    #endregion
}

public class SceneInt
{
    int myInt;

    public SceneInt()
    {
        myInt = 0;
    }

    public int GetCurrent()
    {
        return myInt;
    }

    public int LoadNext()
    {
        myInt++;
        if (myInt > 5)
            myInt = 1;

        return myInt;
    }
}

