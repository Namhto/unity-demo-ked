using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    public static GameManager INSTANCE {
        get {
            if (instance == null) {
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }
            return instance;
        }
    }
    public class Config {
        
        public static float leftLimit = -4f;
    
        public static float rightLimit = 5f;

        public static float startLimit = -3f;
    }

    public enum Event {
        GAME_OVER,
        RESTART,
        SLOW_DOWN_START,
        SLOW_DOWN_END,
        PASSED_OBSTACLE
    }

    [SerializeField]
    private int speed;

    [SerializeField]
    private float slowDownRate;

    [SerializeField]
    private float slowDownDuration;

    [SerializeField]
    private float refillDuration;

    [SerializeField]
    private int spawningZCoord;

    [SerializeField]
    private int distanceBetweenObstacles;

    private bool slowDownAvailable;

    private bool running;

    // Start is called before the first frame update
    void Start()
    {
        running = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getSpeed() {
        return speed;
    }

    public float getSlowDownRate() {
        return slowDownRate;
    }

    public float getSlowDownDuration() {
        return slowDownDuration;
    }

    public float getRefillDuration() {
        return refillDuration;
    }

    public int getSpawningZCoord() {
        return spawningZCoord;
    }

    public int getDistanceBetweenObstacles() {
        return distanceBetweenObstacles;
    }

    public bool isSlowDownAvailable() {
        return slowDownAvailable;
    }

    public void setSlowDownAvailable(bool value) {
        slowDownAvailable = value;
    }

    public bool isRunning() {
        return running;
    }

    public void emitEvent(Event value) {
        switch(value) {
            case Event.GAME_OVER:
                running = false;
                break;
            case Event.RESTART:
                running = true;
                break;
        }
        BroadcastMessage("onEvent", value);
    }
}
