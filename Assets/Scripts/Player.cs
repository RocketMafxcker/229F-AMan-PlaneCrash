using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    float moveSpeed = 1000f;
    float flyUpSpeed = 15;
    float yawPower = 5;
    float rollPower = 2;
    bool checkZone = true;

    Transform target;
    Vector3 offset;
    float followSpeed = 20f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = 10f;
        checkZone = true;
        target = GetComponent<Transform>();
    }
    void FixedUpdate()
    {
        if (checkZone)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(Vector3.forward * moveSpeed * Time.deltaTime);
                moveSpeed++;
                followSpeed++;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                rb.transform.Translate(transform.up * flyUpSpeed * Time.deltaTime);
                rb.AddForce(transform.up * flyUpSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.AddForce(Vector3.down * flyUpSpeed * Time.deltaTime);
            }
            float yaw = Input.GetAxis("Horizontal") * yawPower;
            float roll = Input.GetAxis("Roll") * rollPower;

            rb.AddTorque(transform.forward * yaw);
            rb.AddTorque(transform.up * roll);
        }

        if (checkZone)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        } 
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit!!!");
        if (other.CompareTag("NoMoveZone"))
        {
            checkZone = false;

        }
        if (other.CompareTag("DZone"))
        {
            Destroy(gameObject);
            
        }
    }
}
