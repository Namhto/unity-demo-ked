using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGeneration : MonoBehaviour
{

    [SerializeField]
    private Transform obstaclePrefab;

    private List<ObstacleBatch> batches = new List<ObstacleBatch>();

    private ObstacleBatch lastBatch;

    private float currentSpeed;

    private string[] patterns = {
        "  ********",
        "********  ",
        "***  *****",
        "*****  ***",
        "**  **  **",
        "  **  **  ",
        "**  ******",
        "******  **"
    };

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.INSTANCE.isRunning()) {
            spawnBatch();
            updateObstacles();
        }
    }

    private void init() {
        currentSpeed = GameManager.INSTANCE.getSpeed();
    }

    private void reset() {
        batches.ForEach(batch => batch.delete());
        batches.Clear();
        lastBatch = null;
    }

    private void spawnBatch() {
        if (lastBatch == null) {
            spawnObstacles();
        }
        else if (lastBatch.getDistanceFromSpawnPoint() > GameManager.INSTANCE.getDistanceBetweenObstacles()) {
            spawnObstacles();
        }
    }

    private void updateObstacles() {
        var passedAnObstacle = false;
        for (int i = 0; i < batches.Count; i++) {
            var batch = batches[i];
            if (batch.isOut()) {
                batch.delete();
                batches.Remove(batch);
                passedAnObstacle = true;
            } else {
                batch.update(currentSpeed);
            }
        }
        if (passedAnObstacle) {
            GameManager.INSTANCE.emitEvent(GameManager.Event.PASSED_OBSTACLE);
        }
    }

    private void spawnObstacles() {
        var batch = new ObstacleBatch();
        var pattern = patterns[Random.Range(0, patterns.Length)];
        for (int i = 0; i < pattern.Length; i++) {
            if (pattern[i] == '*') {
                batch.add(
                    Instantiate(obstaclePrefab, new Vector3(i - 4, 2, GameManager.INSTANCE.getSpawningZCoord()), Quaternion.identity, transform)
                );
            }
        }
        lastBatch = batch;
        batches.Add(batch);
    }

    private void onEvent(GameManager.Event value) {
        switch(value) {
            case GameManager.Event.SLOW_DOWN_START:
                currentSpeed = (int) (GameManager.INSTANCE.getSpeed() * GameManager.INSTANCE.getSlowDownRate());
                break;
            case GameManager.Event.SLOW_DOWN_END:
                currentSpeed = GameManager.INSTANCE.getSpeed();
                break;
            case GameManager.Event.RESTART:
                reset();
                init();
                break;
        }
    }
}
