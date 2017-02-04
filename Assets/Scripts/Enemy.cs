using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Unidux;

public sealed class Enemy : SingletonMonoBehaviour<Enemy> {

  public IObservable<Vector2> Movement { get; private set; }
  public IObservable<bool> Firing { get; private set; }

  public enum ActionTypes {
    MOVE,
    FIRE
  }

  public static class ActionCreator {

    public static Action Move(int playerId, Vector3 actionData) {
      return new Action() {
        type = Enemy.ActionTypes.MOVE,
        data = actionData,
        id = playerId
      };
    }

    public static Action Fire() {
      return new Action() {
        type = Enemy.ActionTypes.FIRE
      };
    }
  }

  public class Action {
    public ActionTypes type;
    public Vector3 data;
    public int id;
  }

  // reducers handle state changes
  public static State Reducer(State state, Enemy.Action action) {

    State newState;

    switch (action.type) {
      case Enemy.ActionTypes.MOVE:
        // Immutable state
        newState = state.Clone();
        newState.enemies[action.id].position = action.data;
        return newState;
      case Enemy.ActionTypes.FIRE:
        newState = state.Clone();
        // TODO
        return newState;
    }

    return state;
  }
}
