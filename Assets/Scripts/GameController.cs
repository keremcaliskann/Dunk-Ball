using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    BasketballController basketballController;

    public bool done;
    public Transform done1;
    public Transform done2;
    public Transform done3;

    public GameObject done1Particle;
    public GameObject done2Particle;
    public GameObject done3Particle;

    private void Awake()
    {
        basketballController = FindObjectOfType<BasketballController>();
        done = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public void Done()
    {
        done = true;
        basketballController.sphereCollider.material.bounciness = 0.8f;
        Destroy(Instantiate(done1Particle, done1.position, Quaternion.identity), 5f);
        Destroy(Instantiate(done2Particle, done2.position, Quaternion.identity), 5f);
        Destroy(Instantiate(done3Particle, done3.position, Quaternion.identity), 5f);
        Invoke("Restart", 3f);
    }

    void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnTriggerEnter(Collider other)
    {
        Restart();
    }
}
