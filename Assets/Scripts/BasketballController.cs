using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballController : MonoBehaviour
{
    GameController gameController;
    public Rigidbody basketballRigidbody;

    public GameObject smoke;

    public FixedJoystick fixedJoystick;
    Rigidbody myRigidbody;
    public Collider sphereCollider;

    public Transform pot;

    float moveSpeed;
    int power;
    int shootPowerx;
    int shootPowery;
    public bool inTheAir;
    bool finished;
    bool goal;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        myRigidbody = GetComponent<Rigidbody>();
        inTheAir = false;
        finished = false;
        goal = false;
        moveSpeed = 3f;
        power = 200;
        shootPowerx = 160;
        shootPowery = 350;
    }

    void FixedUpdate()
    {
        if (!gameController.done && !inTheAir)
        {
            Look();
            Move();
        }
    }

    void Look()
    {
        if (Vector3.Distance(pot.position, transform.position) > 2f)
        {
            Vector3 targetPostition = new Vector3(pot.position.x, transform.position.y, pot.position.z);
            transform.LookAt(targetPostition);
        }
    }
    void Move()
    {
        Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        Vector3 direction2 = Vector3.back * fixedJoystick.Horizontal + Vector3.right * fixedJoystick.Vertical;
        Vector3 moveVelocity = direction.normalized * moveSpeed;
        transform.Translate(moveVelocity * Time.fixedDeltaTime);

        basketballRigidbody.AddTorque(direction2 * 100);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground") && !gameController.done)
        {
            goal = false;
            inTheAir = false;
            ClearForces();
            myRigidbody.AddForce(power * Vector3.up);
            Destroy(Instantiate(smoke, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity), 2f);
            basketballRigidbody.AddTorque(RandDirection() * 25);
        }
        if (collision.gameObject.CompareTag("ground") && gameController.done && !finished)
        {
            ClearForces();
            finished = true;
            myRigidbody.AddForce(power * Vector3.up);
        }
        if (collision.gameObject.CompareTag("capsule"))
        {
            basketballRigidbody.angularVelocity /= 1.3f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("basket"))
        {
            if (other.transform.position.y < transform.position.y)
            {
                goal = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("basket"))
        {
            if (other.transform.position.y > transform.position.y)
            {
                if (goal)
                {
                    gameController.Done();
                }
            }
        }
    }

    void ClearForces()
    {
        basketballRigidbody.velocity = Vector3.zero;
        basketballRigidbody.angularVelocity /= 2;
        myRigidbody.velocity = Vector3.zero;
        myRigidbody.angularVelocity = Vector3.zero;
    }

    public void Shoot()
    {
        if (!gameController.done && !inTheAir)
        {
            //float distance = Vector3.Distance(transform.position, pot.position);
            inTheAir = true;
            ClearForces();
            myRigidbody.AddForce(shootPowerx * transform.forward);
            myRigidbody.AddForce(shootPowery * Vector3.up);

            basketballRigidbody.AddTorque(RandDirection() * 800);
        }
    }

    Vector3 RandDirection()
    {
        Vector3 randomDir = Random.onUnitSphere * 3;
        randomDir.y = 0;
        return randomDir;
    }
}
