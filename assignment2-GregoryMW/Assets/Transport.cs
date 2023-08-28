using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    public Transform platform;
    public Vector3 position;
    public float rotation;
    public float rotSpeed;
    public float moveSpeed;
    public bool contact;
    // Start is called before the first frame update
    void Start()
    {
        rotSpeed = 50;
        moveSpeed = 5;
        contact = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (contact)
        {
            platform.position = Vector3.MoveTowards(platform.position, position, moveSpeed * Time.deltaTime);
        }
        if (platform.position.Equals(position))
        {
            moveSpeed = 0;
            rotation += rotSpeed * Time.deltaTime;
            platform.rotation = Quaternion.Euler(0, 0, rotation);
        }
        if (rotation >= 180)
            rotSpeed = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Steel ball")
        {
            contact = true;
        }
    }
}
