using System;
using UnityEngine;

/// <summary>
/// Since unity doesn't flag the Vector3 as serializable, we
/// need to create our own version. This one will automatically convert
/// between Vector3 and SerializableVector3
/// From: http://answers.unity3d.com/questions/956047/serialize-quaternion-or-vector3.html
/// </summary>
[System.Serializable]
public struct SerializableVector3 {

  public float x;
  public float y;
  public float z;

  public SerializableVector3(float rX, float rY, float rZ) {
    x = rX;
    y = rY;
    z = rZ;
  }

  public override string ToString() {
    return String.Format("[{0}, {1}, {2}]", x, y, z);
  }

  // Automatic conversion from SerializableVector3 to Vector3
  public static implicit operator Vector3(SerializableVector3 rValue) {
    return new Vector3(rValue.x, rValue.y, rValue.z);
  }

  // Automatic conversion from Vector3 to SerializableVector3
  public static implicit operator SerializableVector3(Vector3 rValue) {
    return new SerializableVector3(rValue.x, rValue.y, rValue.z);
  }
}

[System.Serializable]
public struct SerializableVector2 {
  public float x;
  public float y;

  public SerializableVector2(float rX, float rY) {
    x = rX;
    y = rY;
  }

  public override string ToString() {
    return String.Format("[{0}, {1}]", x, y);
  }

  // Automatic conversion from SerializableVector2 to Vector2
  public static implicit operator Vector2(SerializableVector2 rValue) {
    return new Vector2(rValue.x, rValue.y);
  }

  // Automatic conversion from Vector2 to SerializableVector2
  public static implicit operator SerializableVector2(Vector2 rValue) {
    return new SerializableVector2(rValue.x, rValue.y);
  }
}
