using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRun : MonoBehaviour
{
    public float MapSpeed = 5.0f;

    void Update()
    {
        // Di chuyển toàn bộ đoạn map
        transform.Translate(Vector3.back * MapSpeed * Time.deltaTime);
    }
}
