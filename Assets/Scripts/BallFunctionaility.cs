using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;

public class BallFunctionaility : MonoBehaviour
{
    [SerializeField] private Collider ballCollider;
    [SerializeField] private float pushForce = 1f;
    [SerializeField] private float groundFriction = 2f;
    [SerializeField] private float wallFriction = 5f;
    [SerializeField] private GameObject continueScreen;
    [SerializeField] private GameObject thanksForPlayingUI;

    private Rigidbody rb;

    [SerializeField] private bool dontStop;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (rb.velocity.magnitude <= 0.001 && dontStop == true)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Time.timeScale = 0.0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            continueScreen.SetActive(true);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 pushDirection = (transform.position - other.transform.position).normalized;
            rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);

            dontStop = true;
        }
        if (other.CompareTag("Hole"))
        {
            dontStop = false;
            if (SceneManager.sceneCount == 4)
            {
                thanksForPlayingUI.SetActive(true);
            }
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Vector3 frictionDirection = -rb.velocity.normalized;
            Vector3 groundFrictionForce = frictionDirection * groundFriction;
            rb.AddForce(groundFrictionForce);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector3 wallFrictionDirection = -rb.velocity.normalized;
            Vector3 wallFriction = wallFrictionDirection * groundFriction;
            rb.AddForce(wallFriction);
        }
    }
}
