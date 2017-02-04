using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Unidux;

public class EnemyRenderer : MonoBehaviour {

  // TODO: Don't hard code this here
  private int enemyId = 0;

  public void Awake() {
    StateManager.Store.AddRenderer(state => {
      transform.position = state.enemies[this.enemyId].position;
    });
  }
}
