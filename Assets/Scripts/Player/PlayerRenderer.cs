using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Unidux;

public class PlayerRenderer : MonoBehaviour {

  public void Awake() {
  }

  public void setPosition(Vector3 position) {
    this.transform.position = position;
  }

}
