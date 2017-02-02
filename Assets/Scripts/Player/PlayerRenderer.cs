using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerRenderer : MonoBehaviour {

  // FIXME: Don't hard code playerid here
  private int playerId;

  public void Awake() {

    this.playerId = 1;

    StateManager.Store.AddRenderer(state => {
      transform.position = state.players[this.playerId].position;
    });
  }
}
