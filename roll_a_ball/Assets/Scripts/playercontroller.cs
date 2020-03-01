using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public Text remainingCount;

    private float distToGround;
    private int countRemaining;
    private Rigidbody rb;
    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        countRemaining = 12;
        SetCountText();
        winText.text = "";
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = Vector3.up * (speed * .8f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            countRemaining -= 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        remainingCount.text = "Remaining: " + countRemaining.ToString();
        if (count >= 12 && countRemaining == 0)
        {
            winText.text = "You Win!";
            Invoke("ChangeScene", 2);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    bool IsGrounded ()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 1);
    }

}

