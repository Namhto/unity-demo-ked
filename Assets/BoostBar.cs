using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostBar : MonoBehaviour
{

    private enum State {
        BOOSTING,
        REFILLING,
        IDLE
    }

    private State state;

    private Image bar;

    private float boostTimer;

    private float refillTimer;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.INSTANCE.isRunning()) {
            switch(state) {
                case State.BOOSTING:
                    consumeBoost();
                    break;
                case State.REFILLING:
                    refillBoost();
                    break;
                case State.IDLE:
                    reset();
                    break;
            }
        }
    }

    private void init() {
        state = State.REFILLING;
        GameManager.INSTANCE.setSlowDownAvailable(false);
        bar = GetComponent<Image>();
        bar.rectTransform.sizeDelta = new Vector2(0, 10);
    }

    private void consumeBoost() {
        boostTimer += Time.deltaTime;
        var ratio = (GameManager.INSTANCE.getSlowDownDuration() - boostTimer) / GameManager.INSTANCE.getSlowDownDuration();
        bar.rectTransform.sizeDelta = new Vector2(Mathf.Lerp(0, 200, ratio), 10);
        if (shouldStopConsumingBoost()) {
            GameManager.INSTANCE.emitEvent(GameManager.Event.SLOW_DOWN_END);
            state = State.REFILLING;
        }
    }

    private void refillBoost() {
        refillTimer += Time.deltaTime;
        var ratio = (GameManager.INSTANCE.getRefillDuration() - refillTimer) / GameManager.INSTANCE.getRefillDuration();
        bar.rectTransform.sizeDelta = new Vector2(Mathf.Lerp(200, 0, ratio), 10);
        if (shouldStopRefillingBoost()) {
            GameManager.INSTANCE.setSlowDownAvailable(true);
            state = State.IDLE;
        }
    }

    private void reset() {
        boostTimer = 0;
        refillTimer = 0;
    }

    private bool shouldStopConsumingBoost() {
        return boostTimer > GameManager.INSTANCE.getSlowDownDuration();
    }

    private bool shouldStopRefillingBoost() {
        return refillTimer > GameManager.INSTANCE.getRefillDuration();
    }

    private void onEvent(GameManager.Event value) {
        switch(value) {
            case GameManager.Event.SLOW_DOWN_START:
                GameManager.INSTANCE.setSlowDownAvailable(false);
                state = State.BOOSTING;
                break;
            case GameManager.Event.RESTART:
                reset();
                init();
                break;
        }
    }
}
