using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Inputs : MonoBehaviour {

  // Singleton.
  public static Inputs Instance { get; private set; }

  public IObservable<Vector2> Movement { get; private set; }
  public IObservable<bool> Firing { get; private set; }

  // Called before Start(), and before any game logic executes
  private void Awake () {
    Instance = this;

    Movement = this.FixedUpdateAsObservable()
      .Select(_ => {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        return new Vector2(x, y).normalized;
      });

    Firing = this.FixedUpdateAsObservable()
      .Select(_ => {
        return Input.GetButtonDown("Fire1");
      });
  }
}
