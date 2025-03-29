using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 0.2f;
    float flySpeed = 10f;
    private Rigidbody rb;
    public PinResetter pinResetter;
    private int frameCount = 0;
    private const int maxFrames = 10;
    private int throwCount = 0; // จำนวนการโยนต่อเฟรม
    private bool frameComplete = false; // เช็คว่าเฟรมนี้จบหรือยัง

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!frameComplete)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(transform.forward * flySpeed, ForceMode.Acceleration);
            }
            float move = Input.GetAxis("Vertical") * speed;
            float turn = Input.GetAxis("Horizontal") * turnSpeed;

            rb.AddForce(transform.up * move, ForceMode.Acceleration);
            rb.AddTorque(Vector3.up * turn, ForceMode.Acceleration);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pin"))
        {
            if (throwCount == 0)
            {
                // หลังจากโยนครั้งแรกเช็คว่าเงื่อนไขสไตรค์หรือไม่
                pinResetter.CalculateScore();
                pinResetter.ResetPins();
                throwCount = 1; // หลังจากโยนครั้งแรก
            }
            else if (throwCount == 1)
            {
                // หลังจากโยนครั้งที่สองเช็คคะแนน
                pinResetter.CalculateScore();
                pinResetter.ResetPins();
                frameCount++;
                throwCount = 0; // รีเซ็ตจำนวนการโยน
                frameComplete = true; // เฟรมนี้จบแล้ว
            }
        }
    }

    void ResetCar()
    {
        transform.position = new Vector3(0, 0.5f, -10);
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
