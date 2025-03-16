using UnityEngine;
using System.Collections;

public class WavesManager : MonoBehaviour
{
    [SerializeField, Space(5)] private CrowdController crowdPrefab;

    [SerializeField, Space(5)] private TextAsset wavesPreset;

    [SerializeField, Space(10), Header("Debug")] private Wave[] waves;

    private void Start()
    {
        WaveParser parser = new WaveParser();
        waves = parser.GetWaves(wavesPreset.ToString());

        StartCoroutine(IterationWaves());
    }

    private IEnumerator IterationWaves()
    {
        foreach(Wave wave in waves)
        {
            yield return new WaitForSeconds(wave.timeTo);
            yield return StartCoroutine(WavePassage(wave));
            
        }
    }

    private IEnumerator WavePassage(Wave wave)
    {
        for (int i = 0; i < wave.crowds.Length; i++)
        {
            CrowdController crowd = Instantiate(crowdPrefab, crowdPrefab.transform.position, Quaternion.identity);
            crowd.InitializeStats(wave.crowds[i]);

            if (i == wave.crowds.Length - 1)
            yield return StartCoroutine(crowd.DestroyInqu());

            else
            StartCoroutine(crowd.DestroyInqu());
        }
    }
}