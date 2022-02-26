using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffects : MonoBehaviour
{
    public double OSCILLATE_SPEED = 10f;

    private bool oscillating = false;
    private bool increasing = true;
    private Vector3 ogScale;

    // Start is called before the first frame update
    void Start()
    {
        ogScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (oscillating)
        {
            if (increasing)
            {
                transform.localScale = new Vector3((float) (transform.localScale.x + OSCILLATE_SPEED * Time.deltaTime),
                                                   (float) (transform.localScale.y + OSCILLATE_SPEED * Time.deltaTime),
                                                   transform.localScale.z);
                increasing &= (ogScale.x * 1.1 > transform.localScale.x);
            } else
            {
                transform.localScale = new Vector3((float)(transform.localScale.x - OSCILLATE_SPEED * Time.deltaTime),
                                                   (float)(transform.localScale.y - OSCILLATE_SPEED * Time.deltaTime),
                                                   transform.localScale.z);
                increasing |= (ogScale.x * .9 > transform.localScale.x);
            }
        }
    }

    public IEnumerator OscillateForSeconds(float seconds)
    {
        oscillating = true;
        yield return new WaitForSeconds(seconds);
        transform.localScale = ogScale;
        increasing = true;
        oscillating = false;
    }
}
