using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lake : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ControlObjects"))
        {
            collision.gameObject.GetComponent<TimeBody>().isRewinding = true;
        }
    }
}
