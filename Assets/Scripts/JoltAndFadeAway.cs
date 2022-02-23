using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JoltAndFadeAway : MonoBehaviour
{
    public float ALPHA_CHANGE;
    public float POSITION_CHANGE;
    public TextMeshProUGUI not;
    public AudioSource roh;
    public AudioSource rohReversed;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            // fading in
            if (not.color.a < 0.99)
            {
                not.color = new Color(not.color.r,
                                      not.color.g,
                                      not.color.b,
                                      not.color.a + ALPHA_CHANGE * Time.deltaTime);

                not.transform.position = new Vector3(not.transform.position.x,
                                                     not.transform.position.y + POSITION_CHANGE * Time.deltaTime,
                                                     not.transform.position.z);
            }
        }
        else if (!active)
        {
            // fading out
            if (not.color.a > 0.01)
            {
                not.color = new Color(not.color.r,
                                      not.color.g,
                                      not.color.b,
                                      not.color.a - ALPHA_CHANGE * Time.deltaTime);

                not.transform.position = new Vector3(not.transform.position.x,
                                                     not.transform.position.y - POSITION_CHANGE * Time.deltaTime,
                                                     not.transform.position.z);
            }
        }
    }

    public void ToggleNot()
    {
        active = !active;
        if (active)
        {
            rohReversed.Play();
        }
        else
        {
            roh.Play();
        }
        
    }
}
