using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using KinematicCharacterController.Examples;

[System.Serializable]
public class ToggleEvent: UnityEvent<bool>
{
}

public class GameController : MonoBehaviour
{
    public ExamplePlayer playerController;
    public TextEffects textScript;
    public NotEffects notScript;
    public ToggleEvent onToggleEvent;
    public bool canToggle = true;

    private bool toggled = false;

    // Start is called before the first frame update
    void Start()
    {
        if (playerController != null)
            StartCoroutine(FreezeThenPlay());
    }

    public IEnumerator FreezeThenPlay()
    {
        StartCoroutine(textScript.OscillateForSeconds(3f));
        yield return new WaitForSeconds(3);
        playerController.ToggleInputCapability(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (canToggle)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                notScript.ToggleNot();
                toggled = !toggled;
                
                if (onToggleEvent != null) 
                    onToggleEvent.Invoke(toggled);

                StartCoroutine(textScript.OscillateForSeconds(.5f));
            }
        }
    }

    public void turnOnGame(bool toggled)
    {
        canToggle = false;
        StartCoroutine(textScript.OscillateForSeconds(2f));
        StartCoroutine(FreezeThenStart());
    }

    public IEnumerator FreezeThenStart()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }
}
