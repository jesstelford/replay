using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Inputs : SingletonMonoBehaviour<Inputs> {

  public IObservable<Vector2> Movement { get; private set; }
  public IObservable<bool> Firing { get; private set; }

  private enum ActionTypes {
    MOVE,
    FIRE
  }

  public static class ActionCreator {

    public static Action Move(Int playerId, Vector3 actionData) {
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

  private class Action {
    public ActionTypes type;
    public Vector2 data;
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
        break;
      case Inputs.ActionTypes.FIRE:
        newState = state.Clone();
        // TODO
        return newState;
        break;
    }

    return state;
  }

  // Called before Start(), and before any game logic executes
  private void Awake () {

    this.Movement = this.FixedUpdateAsObservable()
      .Select(_ => {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        return new Vector2(x, y).normalized;
      });

    this.Firing = this.FixedUpdateAsObservable()
      .Select(_ => {
        return Input.GetButtonDown("Fire1");
      });
  }

  private void Start() {

  }
}
