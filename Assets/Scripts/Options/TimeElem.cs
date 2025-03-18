using UnityEngine;
using UnityEngine.UI;

public class TimePanel : MonoBehaviour
{
    [SerializeField] private Text countupText;

    private void Update()
    {
        countupText.text = Time.time.ToString("F0") + "s";
    }
}
