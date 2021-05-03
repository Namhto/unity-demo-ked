using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffect : MonoBehaviour
{

    private ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        particle.transform.position = new Vector3(0, 0.5f, GameManager.INSTANCE.getSpawningZCoord());
        var pm = particle.main;
        pm.startSpeed = GameManager.INSTANCE.getSpeed() * 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onEvent(GameManager.Event value) {
        switch(value) {
            case GameManager.Event.SLOW_DOWN_START:
                particle.GetComponent<ParticleSystemRenderer>().enabled = false;
                break;
            case GameManager.Event.SLOW_DOWN_END:
                particle.GetComponent<ParticleSystemRenderer>().enabled = true;
                break;
            case GameManager.Event.GAME_OVER:
                particle.Clear();
                particle.Stop();
                break;
            case GameManager.Event.RESTART:
                particle.Play();
                break;
        }
    }
}
