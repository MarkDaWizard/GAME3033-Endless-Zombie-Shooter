using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    protected ZombieStateMachine stateMachine;
    public float UpdateInterval { get; protected set; } = 1f;

    protected State(ZombieStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void IntervalUpdate()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}
