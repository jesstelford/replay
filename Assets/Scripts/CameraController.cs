using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

  // meters per second
  public float scrollSpeed;

  private bool isScrolling;

  // Use this for initialization
  void Start () {
    isScrolling = false;
  }

  // Update is called once per frame
  void Update () {
    // after 2 seconds, start scrolling the camera
    if (!isScrolling && Time.time > 2.0f) {
      isScrolling = true;
    }

    if (isScrolling) {
      transform.position += new Vector3(scrollSpeed * Time.deltaTime, 0, 0);
    }
  }
}
