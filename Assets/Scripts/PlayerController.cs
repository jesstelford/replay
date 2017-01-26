using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  public float speed;
  public Rigidbody2D bullet;

  private Rigidbody2D body;

  void Start() {
    body = GetComponent<Rigidbody2D>();
  }

  void movePlayer() {
    float moveHorizontal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");

    Vector2 movement = new Vector2(moveHorizontal, moveVertical);

    body.AddForce(movement * speed);
  }

  void fireBullet() {
    Vector3 fireFrom = transform.Find("Gun Forward").position;
    Rigidbody2D newBullet = (Rigidbody2D)Instantiate(bullet, fireFrom, Quaternion.identity);
    newBullet.velocity = new Vector3(10, 0, 0);
  }

  void FixedUpdate() {

    movePlayer();

    if (Input.GetButtonDown("Fire1")) {
      fireBullet();
    }
  }
}
