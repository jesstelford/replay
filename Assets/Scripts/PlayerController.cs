using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerController : MonoBehaviour {

  public float speed;
  public Rigidbody2D bullet;

  private Rigidbody2D body;

  void Start() {
    body = GetComponent<Rigidbody2D>();

    Inputs inputs = Inputs.Instance;

    // The observable
    inputs.Movement
      // Only interested in when there are values
      .Where(v => v != Vector2.zero)
      // Observe those values
      .Subscribe(movement => {
        body.AddForce(movement * speed);
      })
      // Stop observing when this game object is destroyed (keeps memory clean)
      .AddTo(this);

    inputs.Firing
      .Where(v => v == true)
      .Subscribe(_ => {
        Vector3 fireFrom = transform.Find("Gun Forward").position;
        Rigidbody2D newBullet = (Rigidbody2D)Instantiate(bullet, fireFrom, Quaternion.identity);
        newBullet.velocity = new Vector3(10, 0, 0);
      })
      .AddTo(this);
  }

}
