using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Reducto;

public class Game : SingletonMonoBehaviour<Game> {

  public GameObject playerObject;

  [Serializable]
  public struct State {
    public PlayerController.State player;
  }

  private Store<State> store;

  // Setup before starting
  override protected void Awake() {
    CheckInstance(); // Singelton stuff

    PlayerController playerScript = (PlayerController)playerObject.GetComponent(typeof(PlayerController));

    var rootReducer = new CompositeReducer<State>(() => {
      // Initial state (note we let the child reducers handle initializing their
      // own slices of state)
      return new State();
    }).Part(
      state => state.player,
      playerScript.reducer()
    );

    this.store = new Store<State>(rootReducer);

    this.store.Subscribe(state => {
      playerScript.subscriber(state.player);
    });

    playerScript.getStateFactory(() => store.GetState().player);
  }

  public Store<State> getStore() {
    return this.store;
  }

  // Use this for initialization
  void Start() {
  }

  // Update is called once per frame
  void Update() {
  }
}
