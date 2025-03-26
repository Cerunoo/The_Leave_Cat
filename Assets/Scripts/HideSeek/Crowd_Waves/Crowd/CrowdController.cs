using UnityEngine;
using System.Collections;
using static PlayerHideFunc;
using System.Collections.Generic;

public class CrowdController : MonoBehaviour, ICrowdController
{
    [SerializeField] private float shiftPos;
    [SerializeField] private float speedDivergence = 5;
    [SerializeField, Space(5)] private AnimationCurve speedCurve;
    [SerializeField] private float decelerationTime;

    [SerializeField, Space(5)] private float timeToSearch;

    private float direction = 0;
    private float timeOffensive;

    [SerializeField, Space(5)] private Animator anim;

    [SerializeField, Space(5)] private GameObject personsWrapper;
    [SerializeField] private float personMultiScaleRange;
    [SerializeField] private GameObject[] personsPrefabs;
    public LayerMask toRightInqu;
    public LayerMask toLeftInqu;

    [SerializeField, Header("Debug")] private Crowd stats;
    private List<HideState> typesChecks;

    #region Interface

    public float Speed => stats.speed;
    public float Direction => direction;
    public float SpeedCurve => speedCurve.Evaluate(timeOffensive / decelerationTime);

    #endregion

    private void Update()
    {
        if (direction != 0) timeOffensive += Time.deltaTime;
    }

    public void InitializeStats(Crowd giveStats)
    {
        direction = 0;
        timeOffensive = 0;

        if (giveStats != null) stats = giveStats;

        typesChecks = new List<HideState>();
        if (stats.house) typesChecks.Add(HideState.House);
        if (stats.bush) typesChecks.Add(HideState.Bush);
        if (stats.roof) typesChecks.Add(HideState.Roof);
        if (stats.pit) typesChecks.Add(HideState.Pit);

        StartCoroutine(waitTimeTo());

        IEnumerator waitTimeTo()
        {
            yield return new WaitForSeconds(stats.timeTo);
            StartOffensive();
        }
    }

    private int ChoosePerson()
    {
        // Генерируем случайное число от 0 до 99
        int randomValue = Random.Range(0, 100);

        // Определяем, какое число вернуть в зависимости от случайного значения
        if (randomValue < 15) // 0-14 (15%)
        {
            return 2;
        }
        else if (randomValue < 45) // 15-44 (30%)
        {
            return 1;
        }
        else // 45-99 (55%)
        {
            return 0;
        }
    }

    private void StartOffensive()
    {
        // "Walk"
        // "Torch" - house;
        // "Shovel" - pit;
        // "Scythe" - roof;
        // "Pitchforks" - bush;

        bool house = false;
        bool pit = false;
        bool roof = false;
        bool bush = false;

        int countPersons = 0;

        // Spawn Walk Persons
        for (int i = 0; i < stats.walk; i++)
        {
            float koafQueue = 1;
            // if (i == 0) koafQueue *= 1f;
            // else if (i == 0) koafQueue *= 1.25f;
            // else if (i == 1) koafQueue *= 1.5f;
            // else if (i == 2) koafQueue *= 1.75f;
            // else if (i == 3) koafQueue *= 2f;
            koafQueue *= 1f + 0.25f * countPersons;
            if (countPersons == 0) koafQueue *= 0.65f;
            koafQueue *= 0.5f;
            koafQueue *= speedDivergence;

            float startKoafQueue = 1 * 0.5f * speedDivergence;

            int skinPerson = ChoosePerson();

            CrowdPersonController person = Instantiate(personsPrefabs[skinPerson], new Vector2(0, 1f), Quaternion.identity).GetComponent<CrowdPersonController>();
            person.transform.parent = personsWrapper.transform;
            person.transform.localScale = new Vector2(0.2924308f * (stats.moveRight ? -1 : 1), 0.2924308f) * 1;
            person.transform.localScale = person.transform.localScale * Random.Range(0.9f, personMultiScaleRange);
            person.InitializeParent(gameObject.GetComponent<ICrowdController>(), GetComponent<CrowdController>(), stats.moveRight ? toRightInqu : toLeftInqu, koafQueue, startKoafQueue);

            if (typesChecks.Contains(HideState.House) == false)
            person.InitializeHideCheck(HideState.Nowhere);

            countPersons++;
        }

        // Spawn Persons
        for (int i = 0; i < typesChecks.Count; i++)
        {
            float koafQueue = 1;
            if (i == 0) koafQueue *= 1f;
            koafQueue *= 1f + 0.25f * (countPersons + 1);
            if (countPersons == 0) koafQueue *= 0.65f;
            koafQueue *= 0.5f;
            koafQueue *= speedDivergence;
            
            float startKoafQueue = 1 * 0.5f * speedDivergence;

            int skinPerson = ChoosePerson();

            CrowdPersonController person = Instantiate(personsPrefabs[skinPerson], new Vector2(0, 1f), Quaternion.identity).GetComponent<CrowdPersonController>();
            person.transform.parent = personsWrapper.transform;
            person.transform.localScale = new Vector2(0.2924308f * (stats.moveRight ? -1 : 1), 0.2924308f) * 1;
            person.transform.localScale = person.transform.localScale * Random.Range(0.9f, personMultiScaleRange);
            person.InitializeParent(gameObject.GetComponent<ICrowdController>(), GetComponent<CrowdController>(), stats.moveRight ? toRightInqu : toLeftInqu, koafQueue, startKoafQueue);

            countPersons++;

            if (stats.house && !house)
            {
                TorchTrigger(person);
                person.ChooseTrigger(TorchTrigger);
                person.InitializeHideCheck(HideState.House);
                house = true;
                continue;
            }
            if (stats.pit && !pit)
            {
                ShovelTrigger(person);
                person.ChooseTrigger(ShovelTrigger);
                person.InitializeHideCheck(HideState.Pit);
                pit = true;
                continue;
            }
            if (stats.roof && !roof)
            {
                ScytheTrigger(person);
                person.ChooseTrigger(ScytheTrigger);
                person.InitializeHideCheck(HideState.Roof);
                roof = true;
                continue;
            }
            if (stats.bush && !bush)
            {
                PitchforksTrigger(person);
                person.ChooseTrigger(PitchforksTrigger);
                person.InitializeHideCheck(HideState.Bush);
                bush = true;
                continue;
            }

            // "Walk"
            // "Torch" - house;
            // "Shovel" - pit;
            // "Scythe" - roof;
            // "Pitchforks" - bush;
        }

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

        // anim.SetTrigger("Offensive");

        if (stats.moveRight)
        {
            DangerNotify.Instance.CallLeftDanger();
        }
        else
        {
            DangerNotify.Instance.CallRightDanger();
        }
    }

    private void TorchTrigger(CrowdPersonController person) => person.anim.SetTrigger("Torch");
    private void ShovelTrigger(CrowdPersonController person) => person.anim.SetTrigger("Shovel");
    private void ScytheTrigger(CrowdPersonController person) => person.anim.SetTrigger("Scythe");
    private void PitchforksTrigger(CrowdPersonController person) => person.anim.SetTrigger("Pitchforks");

    public void CheckPlayer()
    {
        if (GetHideInstance() == HideState.Nowhere && direction != 0)
        {
            DefeatAction();
        }
    }

    public void StartCheckShelter(Shelter shelter)
    {
        if (direction != 0)
        {
            if (typesChecks.Contains(shelter.hideType))
            {
                CheckShelter(shelter.CheckGirl());
            }
        }
    }

    public bool CanCheckShelter(Shelter shelter)
    {
        if (direction != 0)
        {
            if (typesChecks.Contains(shelter.hideType))
            {
                return true;
            }
        }
        return false;
    }

    private void CheckShelter(bool girlInside)
    {
        if (girlInside)
        {
            DefeatAction();
        }
    }

    private void DefeatAction()
    {
        direction = 0;
        PlayerController.Instance.blockMove = true;
        StartCoroutine(FindAnyObjectByType<HistoryController>().CheckDefeat());
    }

    public IEnumerator DestroyInqu()
    {
        yield return new WaitForSeconds(timeToSearch + stats.timeTo);
        // Destroy(gameObject);
    }
}

public interface ICrowdController
{
    public float Speed { get; }
    public float Direction { get; }
    public float SpeedCurve { get; }
}