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
    get { return this.Instance._state = this.Instance._state ?? new State(); }
  }

  public static ReplaySubject<State> Subject {
    get { return this.Instance._subject = this.Instance._subject ?? new ReplaySubject<State>(); }
  }

  public static Store<State> store {
    get {
      if (this.Instance._store == null) {
        this.Instance._store = new Store<State>(this.state);
        this.Instance.CombineReducers(this.Instance._store);
        this.Instance.CombineRenderers(this.Instance._store);
      }
      return this.Instance._store;
    }
  }

  void Update() {
    this.store.Update();
  }
}

public sealed partial class StateManager {
  partial void CombineReducers(Store<State> store) {
    store.AddReducer<Inputs.ActionTypes.MOVE>(Inputs.Reducer);
  }
  partial void CombineRenderers(Store<State> store) {
    store.AddRenderer(state => Subject.OnNext(state));
  }
}
