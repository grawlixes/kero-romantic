using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotEffects : MonoBehaviour
{
    public float ALPHA_CHANGE;
    public float POSITION_CHANGE;
    public float GREEN_CHANGE;

    public TextMeshProUGUI not;
    public AudioSource roh;
    public AudioSource rohReversed;
    public bool active;

    private bool increaseGreen = true;

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

            // color changing
            if (increaseGreen)
            {
                not.color = new Color(not.color.r,
                                      not.color.g + GREEN_CHANGE * Time.deltaTime,
                                      not.color.b,
                                      not.color.a);
                increaseGreen &= not.color.g < 0.99;
            } else
            {
                not.color = new Color(not.color.r,
                                      not.color.g - GREEN_CHANGE * Time.deltaTime,
                                      not.color.b,
                                      not.color.a);
                increaseGreen |= not.color.g < 0.5;
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
