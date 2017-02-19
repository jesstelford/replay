using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerController : MonoBehaviour {

  public float speed;
  public Rigidbody2D bullet;

  public IObservable<Vector3> Move { get; private set; }


  private Transform rendererChild;
  private Transform colliderChild;

  private Rigidbody2D body;

  /*** State ***/
  [Serializable]
  public class State {
    public SerializableVector3 position;

    public State() {
      this.position = new SerializableVector3();
    }

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
    return new Reducto.SimpleReducer<State>(() => {
      // Initial state
      return new State();
    }).When<MoveAction>((state, action) => {

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
    rendererChild.position = state.position;
  }

  /*** Getting state ***/
  public void getStateFactory(Func<State> getState) {
    this.getState = getState;
  }

  // This is set in `getStateFactory` for later use to grab a slice of state
  // we're interested in
  public Func<State> getState;

  /*** Lifecycle methods ***/
  // Called before Start(), and before any game logic executes
  private void Awake () {
    this.rendererChild = this.transform.Find("Renderer");
    this.colliderChild = this.transform.Find("Collider");

    this.Move = this.FixedUpdateAsObservable()
      .Select(_ => colliderChild.position)
      .Where(positionNow => positionNow != this.getState().position);
  }

  private void Start() {

    this.body = this.colliderChild.GetComponent<Rigidbody2D>();

    Inputs inputs = Inputs.Instance;

    // The observable
    inputs.Movement
      // Only interested in when there are values
      .Where(v => v != Vector2.zero)
      // Observe those values
      .Subscribe(movement => {
        this.body.AddForce(movement * speed);
      })
      // Stop observing when this game object is destroyed (keeps memory clean)
      .AddTo(this);

    inputs.Firing
      .Where(v => v == true)
      .Subscribe(_ => {
        // TODO: Fire a redux action creating the new bullet which will have its
        // own Controller / Renderer to handle physics and display
        Vector3 fireFrom = this.colliderChild.Find("Gun Forward").position;
        Rigidbody2D newBullet = (Rigidbody2D)Instantiate(bullet, fireFrom, Quaternion.identity);
        newBullet.velocity = new Vector3(10, 0, 0);
      })
      .AddTo(this);

    this.Move
      .Subscribe(position => {
        ActionCreator.Move(position);
        //StateManager.Store.Dispatch(Inputs.ActionCreator.Move(this.playerId, position));
      });
  }

}

