using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Reducto;

public class Game : MonoBehaviour {

  [Serializable]
  public struct State {
    public PlayerController.State player;
  }

  private Store<State> store;

  // Setup before starting
  void Awake() {

    var rootReducer = new CompositeReducer<State>()
      .Part(
        state => state.player
        PlayerController.reducer(),
      );

    this.store = new Store<State>(rootReducer);

    if (PlayerController.subscriber) {
      var unusb = this.store.Subscribe(state => {
        PlayerController.subscriber(state.player);
      })
    }
  }

  // Use this for initialization
  void Start() {
  }

  // Update is called once per frame
  void Update() {
  }
}
