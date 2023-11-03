using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AudioSource movementSound;

    public Transform orientation;

    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;

    [SerializeField] private GameObject TutorialText;

    private bool soundCurrentlyPlaying = false;
    private bool firstTimeMoving = true;

    private Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        speed = 8;

        Time.timeScale = 1.0f;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * speed, ForceMode.Force);

        if ((horizontalInput != 0 || verticalInput != 0) && soundCurrentlyPlaying == false)
        {
            StartCoroutine(PlayerWalkingSound());
        }
        if ((horizontalInput != 0 || verticalInput != 0) && firstTimeMoving == true)
        {
            StartCoroutine(FirstTimeMessage());
        }
    }

    private IEnumerator PlayerWalkingSound()
    {
        soundCurrentlyPlaying = true;

        movementSound.Play();

        yield return new WaitForSeconds(movementSound.clip.length + 0.5f);

        soundCurrentlyPlaying = false;
    }
    private IEnumerator FirstTimeMessage()
    {
        TutorialText.SetActive(true);

        yield return new WaitForSeconds(3);

        TutorialText.SetActive(false);

        firstTimeMoving = false;
    }
}
