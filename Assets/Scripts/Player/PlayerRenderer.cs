using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Unidux;

public class PlayerRenderer : MonoBehaviour {

  public void Awake() {

    StateManager.Store.AddRenderer(state => {
      transform.position = state.players[this.playerId].position;
    });
  }

  /*** Subscriber ***/
  public void subscriber(PlayerController.State state) {
    transform.position = state.players[this.playerId].position;
  }

}
