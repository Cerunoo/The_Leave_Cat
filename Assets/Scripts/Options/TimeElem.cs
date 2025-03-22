using UnityEngine;
using UnityEngine.UI;

public class TimePanel : MonoBehaviour
{
    [SerializeField] private Text countupText;

    private float time;

    private void Update()
    {
        time += Time.deltaTime;
        countupText.text = time.ToString("F0") + "s";
    }
}
