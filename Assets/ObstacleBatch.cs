using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBatch {

    private List<Transform> items = new List<Transform>();

    public void add(Transform item) {
        items.Add(item);
    }

    public void update(float speed) {
        if (GameManager.INSTANCE.isRunning()) {
            items.ForEach(item => {
                item.Translate(Vector3.back * Time.deltaTime * speed, Space.World);
            });
        }
    }

    public bool isOut() {
        return items[0].position.z < GameManager.Config.startLimit;
    }

    public void delete() {
        items.ForEach(item => Object.Destroy(item.gameObject));
    }

    public float getDistanceFromSpawnPoint() {
        return GameManager.INSTANCE.getSpawningZCoord() - items[0].position.z;
    }
}