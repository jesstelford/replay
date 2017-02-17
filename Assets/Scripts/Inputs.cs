using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public sealed class Inputs : SingletonMonoBehaviour<Inputs> {

  public IObservable<Vector2> Movement { get; private set; }
  public IObservable<bool> Firing { get; private set; }

  // Called before Start(), and before any game logic executes
  new private void Awake () {

    Instance.Movement = Instance.FixedUpdateAsObservable()
      .Select(_ => {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        return new Vector2(x, y).normalized;
      });

    Instance.Firing = Instance.FixedUpdateAsObservable()
      .Select(_ => {
        return Input.GetButtonDown("Fire1");
      });
  }

  private void Start() {

  }
}
