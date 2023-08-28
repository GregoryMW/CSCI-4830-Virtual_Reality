using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Transform player;
    public float rotation;
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rotateSpeed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        rotation += rotateSpeed * Time.deltaTime;
        player.rotation = Quaternion.Euler(0, rotation, 0);
    }
}
