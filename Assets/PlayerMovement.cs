using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private int lateralSpeed = 15;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.INSTANCE.isRunning()) {
            handleInput();
        } else if (shouldRestart()) {
            GameManager.INSTANCE.emitEvent(GameManager.Event.RESTART);
        }
    }
    
    void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "obstacle") {
            GameManager.INSTANCE.emitEvent(GameManager.Event.GAME_OVER);
        }
    }

    private void handleInput() {
        if (Input.GetKey(KeyCode.Q)) {
            moveLeft();
        }
        if (Input.GetKey(KeyCode.D)) {
            moveRight();
        }
        if (Input.GetKey(KeyCode.S)) {
            activateSlowDown();
        }
    }

    private bool shouldRestart() {
        return Input.GetKey(KeyCode.R);
    }

    private void reset() {
        transform.position = new Vector3(0, 1, 0);
    }

    private void moveLeft() {
        transform.Translate(Vector3.left * Time.fixedDeltaTime * lateralSpeed, Space.World);
        if (transform.position.x < GameManager.Config.leftLimit) {
            transform.position = new Vector3(GameManager.Config.leftLimit, transform.position.y, transform.position.z);
        }
    }

    private void moveRight() {
        transform.Translate(Vector3.right * Time.fixedDeltaTime * lateralSpeed, Space.World);
        if (transform.position.x > GameManager.Config.rightLimit) {
            transform.position = new Vector3(GameManager.Config.rightLimit, transform.position.y, transform.position.z);
        }
    }

    private void activateSlowDown() {
        if (GameManager.INSTANCE.isSlowDownAvailable()) {
            GameManager.INSTANCE.emitEvent(GameManager.Event.SLOW_DOWN_START);
        }
    }

    private void onEvent(GameManager.Event value) {
        switch(value) {
            case GameManager.Event.RESTART:
                reset();
                break;
        }
    }
}
