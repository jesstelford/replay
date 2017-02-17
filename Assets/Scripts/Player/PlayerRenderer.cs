using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour {

  public void Awake() {
  }

  public void setPosition(Vector3 position) {
    this.transform.position = position;
  }

}
