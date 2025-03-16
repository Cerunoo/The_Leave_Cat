using UnityEngine;
using System.Collections;
using static PlayerHideFunc;
using System.Collections.Generic;

public class CrowdController : MonoBehaviour
{
    [SerializeField] private float shiftPos;
    [SerializeField, Space(5)] private AnimationCurve speedCurve;
    [SerializeField] private float decelerationTime;

    [SerializeField, Space(5)] private float timeToSearch;

    private float direction = 0;
    private float timeOffensive;

    [SerializeField, Space(5)] private Animator anim;

    [SerializeField, Header("Debug")] private Crowd stats;
    private List<HideState> typesChecks;

    private void Update()
    {
        transform.Translate(new Vector2(stats.speed * direction * speedCurve.Evaluate(timeOffensive / decelerationTime), 0) * Time.deltaTime);
        if (direction != 0) timeOffensive += Time.deltaTime;
    }

    public void InitializeStats(Crowd giveStats)
    {
        direction = 0;
        timeOffensive = 0;

        if (giveStats != null) stats = giveStats;

        typesChecks = new List<HideState>();
        if (stats.House) typesChecks.Add(HideState.House);
        if (stats.Bush) typesChecks.Add(HideState.Bush);
        if (stats.Roof) typesChecks.Add(HideState.Roof);
        if (stats.Pit) typesChecks.Add(HideState.Pit);

        StartCoroutine(waitTimeTo());

        IEnumerator waitTimeTo()
        {
            yield return new WaitForSeconds(stats.timeTo);
            StartOffensive();
        }
    }

    private void StartOffensive()
    {
        // Transform

        Vector2 pos = PlayerController.Instance.transform.position;

        if (stats.moveRight)
        {
            pos.x -= shiftPos;
            direction = 1;
        }
        else
        {
            pos.x += shiftPos;
            direction = -1;
        }

        transform.position = pos;

        // Render

        anim.SetTrigger("Offensive");

        if (stats.moveRight)
        {
            DangerNotify.Instance.CallLeftDanger();
        }
        else
        {
            DangerNotify.Instance.CallRightDanger();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shelter")
        {
            direction *= 0.5f;
        }

        if (collision.tag == "Player" && GetHideInstance() == HideState.Nowhere && direction != 0)
        {
            // "Defeat, Girl Nowhere"
            Debug.Log("Defeat, Girl Nowhere");
            Debug.LogError("Defeat, Girl Nowhere");
        }

        if (collision.tag == "Shelter" && direction != 0)
        {
            Shelter shelter = collision.GetComponent<Shelter>();
            if (typesChecks.Contains(shelter.hideType))
            {
                CheckShelter(shelter.CheckGirl());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Shelter")
        {
            direction *= 2f;
        }
    }

    private void CheckShelter(bool girlInside)
    {
        if (girlInside)
        {
            // "Defeat, Girl Inside"
            Debug.Log("Defeat, Girl Inside");
            Debug.LogError("Defeat, Girl Inside");
        }
    }

    public IEnumerator WaveIsOver()
    {
        yield return new WaitForSeconds(timeToSearch);
        Destroy(gameObject);
    }
}