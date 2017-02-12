using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Unidux;

public class PlayerController<ParentState> : MonoBehaviour {

  /*** State ***/
  [Serializable]
  public class State {
    public SerializableVector3 position;

    public State State(State oldState) {
      // Make a copy
      this.position = new SerializableVector3(oldState.position);
    }
  }

  /*** Actions ***/
  public struct MoveAction { }

  /*** Action Creators ***/
  public static class ActionCreator {
    public static MoveAction Move() {
      return new MoveAction;
    }
  }

  /*** Reducers ***/
  public Reducto.SimpleReducer<ParentState> reducer() {
    return new Reducto.SimpleReducer<State>()
      .When<MoveAction>((state, action) => {

        // TODO: Only clone / update if values are different

        // Clone current state object
        State newState = new State(state);

        // 2. Update with latest values
        newState.position = action.position;

        // 3. Return new state copy
        return newState;
      });
  }

  /*** Subscriber ***/
  public ?? subscriber() {
    // TODO
  }
}










public class PlayerController : MonoBehaviour {

  public float speed;
  public Rigidbody2D bullet;

  public IObservable<Vector3> Move { get; private set; }

  private Rigidbody2D body;

  private int playerId;

  // Called before Start(), and before any game logic executes
  private void Awake () {

    // TODO: Don't hard code this
    this.playerId = 1;

    this.Move = this.FixedUpdateAsObservable()
      .Select(_ => {
        return transform.position;
      })
      .Where(positionNow => positionNow != StateManager.State.players[this.playerId].position);
  }

  void Start() {
    body = GetComponent<Rigidbody2D>();

    Inputs inputs = Inputs.Instance;

    // The observable
    inputs.Movement
      // Only interested in when there are values
      .Where(v => v != Vector2.zero)
      // Observe those values
      .Subscribe(movement => {
        body.AddForce(movement * speed);
      })
      // Stop observing when this game object is destroyed (keeps memory clean)
      .AddTo(this);

    inputs.Firing
      .Where(v => v == true)
      .Subscribe(_ => {
        // TODO: Fire a redux action creating the new bullet which will have its
        // own Controller / Renderer to handle physics and display
        Vector3 fireFrom = transform.Find("Gun Forward").position;
        Rigidbody2D newBullet = (Rigidbody2D)Instantiate(bullet, fireFrom, Quaternion.identity);
        newBullet.velocity = new Vector3(10, 0, 0);
      })
      .AddTo(this);

    this.Move
      .Subscribe(position => {
        StateManager.Store.Dispatch(Inputs.ActionCreator.Move(this.playerId, position));
      });
  }

}
