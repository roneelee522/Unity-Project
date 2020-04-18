using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Textcontrol : MonoBehaviour
{
    public Text hintText;
    // Start is called before the first frame update
    void Start()
    {
        hintText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hintText.text = "";
            Destroy(this);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hintText.text = "Press 'Space' to Slide!";
        }

    }
}