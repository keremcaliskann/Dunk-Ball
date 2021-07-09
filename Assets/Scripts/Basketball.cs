using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basketball : MonoBehaviour
{
    public Transform basketballController;

    void FixedUpdate()
    {
        transform.position = basketballController.position;
    }
}
