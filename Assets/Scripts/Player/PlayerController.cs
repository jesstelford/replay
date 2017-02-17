using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerController : MonoBehaviour {

  private PlayerRenderer rendererChild;

  private void Start() {
    this.rendererChild = this.GetComponentsInChildren<PlayerRenderer>()[0];
  }

  /*** State ***/
  [Serializable]
  public class State {
    public SerializableVector3 position;

    public State(State oldState) {
      // Make a copy
      this.position = oldState.position;
    }
  }

  /*** Actions ***/
  public struct MoveAction {
    public SerializableVector3 position;
  }

  /*** Action Creators ***/
  public static class ActionCreator {

    public static void Move(SerializableVector3 newPosition) {
      Game.Instance.getStore().Dispatch(
        new MoveAction {
          position = newPosition
        }
      );
    }

  }

  /*** Reducers ***/
  public Reducto.SimpleReducer<State> reducer() {
    return new Reducto.SimpleReducer<State>()
      .When<MoveAction>((state, action) => {

        // Clone current state object
        State newState = new State(state);

        // Update with latest values
        newState.position = action.position;

        // Return new state copy
        return newState;
      });
  }

  /*** Subscriber ***/
  public void subscriber(State state) {
    // TODO: Compose the state slicer & subscriber to pass this data straight down
    rendererChild.setPosition(state.position);
  }

  /*** Getting state ***/
  public void getStateFactory(Func<State> getState) {
    this.getState = getState;
  }

  // This is set in `getStateFactory` for later use to grab a slice of state
  // we're interested in
  public Func<State> getState;
}

