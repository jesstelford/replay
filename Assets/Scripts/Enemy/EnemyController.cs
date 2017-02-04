using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Unidux;

public class EnemyController : MonoBehaviour {

  public Rigidbody2D bullet;
  public float amplitude;
  public float hertz;

  private float startTime;
  private int enemyId = 0;

  void Start() {
    this.startTime = Time.time;
  }

  void Update() {

    float elapsedTime = Time.time - this.startTime;
    float x = elapsedTime * (2 * Mathf.PI * this.hertz);
    float yPos = Mathf.Sin(x) * this.amplitude;

    transform.position = new Vector3(transform.position.x, yPos, transform.position.y);

    StateManager.Store.Dispatch(Enemy.ActionCreator.Move(this.enemyId, transform.position));
  }

}
