using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  public float speed;

  private Rigidbody2D body;

  void Start() {
    body = GetComponent<Rigidbody2D>();
    body.constraints = RigidbodyConstraints2D.FreezeRotation;
  }

  void FixedUpdate() {
    float moveHorizontal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");

    /*
    // Can't move further forward than 3/4 of the screen (ish)
    if (moveHorizontal > 0 && gameObject.position.x > camera.GetCenter() + (camera.GetWidth() * 0.75)) {
      moveHorizontal = 0;
    }
    */

    Vector2 movement = new Vector2(moveHorizontal, moveVertical);



    body.AddForce(movement * speed);

    if (Input.GetButtonDown("Fire1")) {
      // TODO: Firing logic
    }
  }
}
