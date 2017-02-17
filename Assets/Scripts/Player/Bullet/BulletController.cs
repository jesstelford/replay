using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Bullet : MonoBehaviour {
/*
  public IObservable<Vector3> Move { get; private set; }

  // When the bullet leaves the area it's allowed to be in,
  // we destroy it
  void OnTriggerExit2D(Collider2D other) {
    if (other.name == "Bullet Limit") {
      Destroy(gameObject);
    }
  }

  // Called before Start(), and before any game logic executes
  private void Awake () {

    // TODO: Don't hard code this
    this.bulletId = 1;

    this.Move = this.FixedUpdateAsObservable()
      .Select(_ => {
        return transform.position;
      })
      .Where(positionNow => positionNow != StateManager.State.bullets[this.bulletId].position);
  }

  void Start() {

    Inputs inputs = Inputs.Instance;

    this.Move
      .Subscribe(position => {
        StateManager.Store.Dispatch(Inputs.ActionCreator.Move(this.playerId, position));
      });
  }
*/
}

