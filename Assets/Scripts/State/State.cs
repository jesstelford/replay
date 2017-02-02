using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class State : StateBase<State> {

  [Serializable]
  public Dictionary<int, PlayerState> players;

  [Serializable]
  private class PlayerState {
    public Vector3 position;
  }

  public State() {
    this.players = new Dictionary<int, PlayerState>();
  }
}
