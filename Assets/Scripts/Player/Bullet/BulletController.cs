using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

  // When the bullet leaves the area it's allowed to be in,
  // we destroy it
  void OnTriggerExit2D(Collider2D other) {
    if (other.name == "Bullet Limit") {
      Destroy(gameObject);
    }
  }

}
