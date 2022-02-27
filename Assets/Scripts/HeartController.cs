using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    public float APPROACH_SPEED = .005f;
    public float SPIN_SPEED = .1f;
    public GameObject target;
    public bool approaching = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (approaching && target.transform.localPosition.y > gameObject.transform.localPosition.y)
        {
            gameObject.transform.localPosition =
                new Vector3(gameObject.transform.localPosition.x,
                            gameObject.transform.localPosition.y + APPROACH_SPEED * Time.deltaTime,
                            gameObject.transform.localPosition.z);
        }

        gameObject.transform.localRotation *= Quaternion.Euler(Vector3.up * SPIN_SPEED * Time.deltaTime);
    }
}
