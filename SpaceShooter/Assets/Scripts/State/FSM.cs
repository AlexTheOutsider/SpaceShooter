using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FSM<TContext>
{
    private readonly TContext _context;
    private readonly Dictionary<Type, State> _stateCache = new Dictionary<Type, State>();

    public State CurrentState { get; private set; }
    private State _pendingState;

    public FSM(TContext context)
    {
        _context = context;
    }

    public void Update()
    {
        // Handle any pending transition if someone called TransitionTo externally (although they probably shouldn't)
        PerformPendingTransition();
        Debug.Assert(CurrentState != null,
            "Updating FSM with null current state. Did you forget to transition to a starting state?");
        CurrentState.Update();
        PerformPendingTransition();
    }

    public void TransitionTo<TState>() where TState : State
    {
        _pendingState = GetOrCreateState<TState>();
        Debug.Log("Transition to new state: " + typeof(TState).Name);
    }

    private void PerformPendingTransition()
    {
        if (_pendingState != null)
        {
            if (CurrentState != null) CurrentState.OnExit();
            CurrentState = _pendingState;
            CurrentState.OnEnter();
            _pendingState = null;
        }
    }

    // A helper method to help with managing the caching of the state instances
    private TState GetOrCreateState<TState>() where TState : State
    {
        State state;
        if (_stateCache.TryGetValue(typeof(TState), out state))
        {
            return (TState) state;
        }
        else
        {
            // This activator business is required to create instances of states
            // using only the type
            var newState = Activator.CreateInstance<TState>();
            newState.Parent = this;
            newState.Init();
            _stateCache[typeof(TState)] = newState;
            return newState;
        }
    }

    // We define the base class for states inside the FSM class so it's tied to the
    // context type of the FSM class. This way you can't try to transition to a state
    // that is for a different type of context.
    public abstract class State
    {
        internal FSM<TContext> Parent { get; set; }

        protected TContext Context
        {
            get { return Parent._context; }
        }

        protected void TransitionTo<TState>() where TState : State
        {
            Parent.TransitionTo<TState>();
        }

        // This is called once when the state is first created (think of it like Unity's Awake)
        public virtual void Init()
        {
        }

        // This is called whenever the state becomes active (think of it like Unity's Start)
        public virtual void OnEnter()
        {
        }

        // this is called whenever the state becomes inactive
        public virtual void OnExit()
        {
        }

        // This is your standard update method where most of your work should go
        public virtual void Update()
        {
        }

        // called when the state machine is cleared, and where you should clear resources
        public virtual void CleanUp()
        {
        }
    }
}