using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameController gameController;
    public Transform basketball;
    public Transform basket;
    float delay;

    private void Start()
    {
        delay = 5f;
    }

    void FixedUpdate()
    {
        if (!gameController.done)
        {
            Vector3 basketballPostition = new Vector3(basketball.position.x, 1f, basketball.position.z);
            transform.position = Vector3.Lerp(transform.position, basketballPostition, 1 / delay);

            Vector3 basketballRotation = new Vector3(basket.position.x, transform.position.y, basket.position.z);
            transform.LookAt(basketballRotation);
        }
    }
}
