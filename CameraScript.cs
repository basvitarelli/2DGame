using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    private Transform player;

    public float minX, maxX;

    // Use this for initialization
    void Start () {
        player = GameObject.Find ("player").transform;
    }

    // Update is called once per frame
    void Update () {
        if (GameObject.Find("player").GetComponent<PlayerScript>().imAlive)  {
            Vector3 temp = transform.position;
            temp.x = player.position.x;
            transform.position = temp;
        }
    }
}
//CameraFollow