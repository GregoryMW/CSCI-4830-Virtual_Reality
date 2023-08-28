using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessSound : MonoBehaviour
{
    [SerializeField] AudioClip chessSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioSource.PlayClipAtPoint(chessSound, collision.contacts[0].point, collision.relativeVelocity.magnitude);
    }
}
