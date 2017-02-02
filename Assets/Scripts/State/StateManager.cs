using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Unidux;

public sealed partial class StateManager : SingletonMonoBehaviour<StateManager> {

  partial void CombineReducers(Store<State> store);

  private ReplaySubject<State> _subject;
  private Store<State> _store;
  private State _state;

  public static State State {
    get { return Instance._state = Instance._state ?? new State(); }
  }

  public static ReplaySubject<State> Subject {
    get { return Instance._subject = Instance._subject ?? new ReplaySubject<State>(); }
  }

  public static Store<State> Store {
    get {
      if (Instance._store == null) {
        Instance._store = new Store<State>(State);
        Instance.CombineReducers(Instance._store);
      }
      return Instance._store;
    }
  }

  void Update() {
    Store.Update();
  }
}

public sealed partial class StateManager {
  partial void CombineReducers(Store<State> store) {
    store.AddReducer<Inputs.Action>(Inputs.Reducer);
  }
}
