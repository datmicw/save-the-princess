using UnityEngine;

public class MapRun : MonoBehaviour
{
    public float mapSpeed = 5.0f; // Tốc độ di chuyển map
    private bool isRunning = true; // Trạng thái chạy/dừng map

    void Update()
    {
        if (isRunning)
        {
            // Di chuyển toàn bộ đoạn map
            MoveMap();
        }
    }

    private void MoveMap() // Di chuyển map
    {
        transform.Translate(Vector3.back * mapSpeed * Time.deltaTime);
    }
    // Dừng map
    public void StopMap()
    {
        isRunning = false;
        Debug.Log("Map stopped.");
    }

}
