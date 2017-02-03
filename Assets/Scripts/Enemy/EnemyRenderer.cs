using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Unidux;

public class EnemyRenderer : MonoBehaviour {

  public void Awake() {
    StateManager.Store.AddRenderer(state => {
      Debug.Log("Enemy Renderer");
    });
  }
}
