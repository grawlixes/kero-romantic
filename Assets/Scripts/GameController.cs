using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using KinematicCharacterController.Examples;
using TMPro;

public class GameController : MonoBehaviour
{
    public ExamplePlayer playerController;
    public TextEffects textScript;
    public NotEffects notScript;
    public UnityEvent onToggleEvent;
    public bool canToggle = true;
    public bool shouldEnableOnStart = true;
    public GameObject level;

    // Finale
    public TextMeshProUGUI characterDialogue;
    public TextMeshProUGUI gfDialogue;
    public TextMeshProUGUI instructions;
    public TextEffects gfTextScript;
    public NotEffects gfNotScript;
    public HeartController heart;
    public int textIndex = 0;

    private bool exitOnEsc = false;

    private bool toggled = false;

    // Start is called before the first frame update
    void Start()
    {
        if (playerController != null && shouldEnableOnStart)
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
                    onToggleEvent.Invoke();

                StartCoroutine(textScript.OscillateForSeconds(.5f));

                if (heart != null)
                    canToggle = false;

                if (gfDialogue != null)
                    gfDialogue.gameObject.SetActive(false);
            }
        } else if (!shouldEnableOnStart)
        {
            // finale updates
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (textIndex != FinaleConstants.INDICES.Length)
                {
                    TextMeshProUGUI textBox = FinaleConstants.INDICES[textIndex] == 0 ? characterDialogue : gfDialogue;
                    textBox.gameObject.SetActive(true);
                    string text = FinaleConstants.TEXT[textIndex];
                    textBox.text = text;

                    TextMeshProUGUI otherTextBox = FinaleConstants.INDICES[textIndex] == 1 ? characterDialogue : gfDialogue;
                    otherTextBox.gameObject.SetActive(false);
                    textIndex += 1;
                } else
                {
                    shouldEnableOnStart = true;
                    canToggle = true;
                    gfNotScript.ToggleNot();
                    instructions.text = "Press E to make it real. For real this time.";
                }
            }
        } else if (exitOnEsc && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void turnOnGame()
    {
        canToggle = false;
        StartCoroutine(textScript.OscillateForSeconds(2f));
        StartCoroutine(FreezeThenStart());
    }

    public void TogglePlatforms()
    {
        ExampleMovingPlatform[] platforms = level.GetComponentsInChildren<ExampleMovingPlatform>();

        foreach (ExampleMovingPlatform p in platforms)
        {
            p.ToggleMoving();
        }
    }

    public IEnumerator FreezeThenStart()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }

    public void ToggleActive(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }

    public void FrogLove()
    {
        heart.approaching = true;
        instructions.text = "Thank you for playing! Press ESC to quit.";
        exitOnEsc = true;
        StartCoroutine(Credits());
    }

    private IEnumerator Credits()
    {
        characterDialogue.gameObject.SetActive(true);

        foreach (string s in FinaleConstants.CREDITS)
        {
            characterDialogue.text = s;
            yield return new WaitForSeconds(4);
        }
    }
}
