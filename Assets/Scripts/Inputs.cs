using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Unidux;


public sealed class Inputs : SingletonMonoBehaviour<Inputs> {

  public IObservable<Vector2> Movement { get; private set; }
  public IObservable<bool> Firing { get; private set; }

  public enum ActionTypes {
    MOVE,
    FIRE
  }

  public static class ActionCreator {

    public static Action Move(int playerId, Vector3 actionData) {
      return new Action() {
        type = Inputs.ActionTypes.MOVE,
        data = actionData,
        id = playerId
      };
    }

    public static Action Fire() {
      return new Action() {
        type = Inputs.ActionTypes.FIRE
      };
    }
  }

  public class Action {
    public ActionTypes type;
    public Vector3 data;
    public int id;
  }

  // reducers handle state changes
  public static State Reducer(State state, Inputs.Action action) {

    State newState;

    switch (action.type) {
      case Inputs.ActionTypes.MOVE:
        // Immutable state
        newState = state.Clone();
        newState.players[action.id].position = action.data;
        return newState;
      case Inputs.ActionTypes.FIRE:
        newState = state.Clone();
        // TODO
        return newState;
    }

    return state;
  }

  // Called before Start(), and before any game logic executes
  new private void Awake () {

    Instance.Movement = Instance.FixedUpdateAsObservable()
      .Select(_ => {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        return new Vector2(x, y).normalized;
      });

    Instance.Firing = Instance.FixedUpdateAsObservable()
      .Select(_ => {
        return Input.GetButtonDown("Fire1");
      });
  }

  private void Start() {

  }
}
