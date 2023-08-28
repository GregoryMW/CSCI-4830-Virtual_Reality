using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class Button : MonoBehaviour
{
    public UnityAction<VRHand> buttonPressed;
    [SerializeField] Color inactiveColor;
    [SerializeField] Color activeColor;
    [SerializeField] AudioClip buttonAccept;
    public TMP_Text label;
    public TMP_Text count;
    public bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = inactiveColor;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;
        VRHand hand = rb.GetComponent<VRHand>();
        if (hand == null) return;
        if (label.text == count.text)
        {
            int i = int.Parse(label.text);
            i += 1;
            count.text = i.ToString();
            isActive = true;
            GetComponent<Renderer>().material.color = activeColor;
            AudioSource.PlayClipAtPoint(buttonAccept, other.transform.position);
        }
    }
}
