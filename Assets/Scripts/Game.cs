using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Reducto;

public class Game : SingletonMonoBehaviour<Game> {

  public gameObject playerObject;

  [Serializable]
  private struct State {
    public PlayerController.State player;
  }

  private Store<State> store;

  // Setup before starting
  void Awake() {

    PlayerController playerScript = (PlayerController)playerObject.GetComponent(typeof(PlayerController));

    var rootReducer = new CompositeReducer<State>()
      .Part(
        state => state.player
        playerScript.reducer(),
      );

    this.store = new Store<State>(rootReducer);

    if (playerScript.subscriber) {
      var unusb = this.store.Subscribe(state => {
        playerScript.subscriber(state.player);
      })
    }

    if (playerScript.getStateFactory) {
      playerScript.getStateFactory(() => store.GetState().player);
    }
  }

  Store<State> getStore() {
    return this.store;
  }

  // Use this for initialization
  void Start() {
  }

  // Update is called once per frame
  void Update() {
  }
}
