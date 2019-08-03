using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour {
    public int counter = 1;
    private TextMeshProUGUI text;

    private void Start() {
        text = GetComponentInChildren<TextMeshProUGUI>();
        SetCounter(counter);
    }

    public void SetCounter(int amount) {
        counter = amount;
        text.text = "x" + counter;
    }

    public void DecrementCounter() {
        SetCounter(counter - 1);
    }

    public void IncreaseCounter() {
        SetCounter(counter + 1);
    }
}