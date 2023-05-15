using IWantToWorkAtComplexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private Health targetPrefab;
    [SerializeField] private float minimumSpawnDistance;
    [SerializeField] private float maximumSpawnDistance;
    [SerializeField] private int movingTargetChance = 475;
    [SerializeField] private int circleTargetChance = 495;
    [SerializeField] private int maxRollNum = 500;

    private LevelWinManager levelWinManager;
    private GameObject player;
    private Pool<Health> targetPool;
    private int idleAnimTriggerID;
    private int backandForthAnimTriggerID;
    private int circleAnimTriggerID;

    // Start is called before the first frame update
    void Start()
    {
        levelWinManager = FindFirstObjectByType<LevelWinManager>();
        player = GameObject.FindWithTag("Player");
        targetPool = LazyPoolerUtility.GetSimplePool<Health>(targetPrefab.gameObject);
        idleAnimTriggerID = Animator.StringToHash("Idle");
        backandForthAnimTriggerID = Animator.StringToHash("BackAndForth");
        circleAnimTriggerID = Animator.StringToHash("Circle");

        for (int i = 0; i < 25; i++)
        {
            SpawnTarget();
        }

    }

    private void SpawnTarget()
    {
        Health newTarget = LazyPoolerUtility.GetSimplePooledObject<Health>(targetPrefab.gameObject);

        Vector3 originPosition = player.transform.position;
        Vector3 randomValue = Random.insideUnitSphere * maximumSpawnDistance;
        randomValue.y = Random.Range(-3, 5);

        if (randomValue.x < minimumSpawnDistance && randomValue.x > -minimumSpawnDistance)
            randomValue.x = minimumSpawnDistance * (randomValue.x > 0 ? 1 : -1);

        if (randomValue.z < minimumSpawnDistance && randomValue.z > -minimumSpawnDistance)
            randomValue.z = minimumSpawnDistance * (randomValue.z > 0 ? 1 : -1);

        originPosition += randomValue;

        newTarget.transform.position = originPosition;
        newTarget.transform.LookAt(player.transform);

        newTarget.gameObject.SetActive(true);
        newTarget.OnDeath += NewTarget_OnDamage;

        Animator animator = newTarget.GetComponent<Animator>();
        int triggerID = 0;
        int randomRoll = Random.Range(1, maxRollNum);
        if (randomRoll > 0)
            triggerID = idleAnimTriggerID;
        if (randomRoll > movingTargetChance)
        {
            triggerID = backandForthAnimTriggerID;
            newTarget.OnDeath += ExtraBonusPoints;
        }
        if (randomRoll > circleTargetChance)
        {
            triggerID = circleAnimTriggerID;
            newTarget.OnDeath += SuperExtraBonusPoints;

        }


        animator.SetTrigger(triggerID);

    }

    private void NewTarget_OnDamage(IDamageable target)
    {
        target.OnDeath -= NewTarget_OnDamage;
        targetPool.Release(target.gameObject.GetComponent<Health>());
        levelWinManager.AddPoints(1);
        StartCoroutine(RespawnTarget());
    }

    private void ExtraBonusPoints(IDamageable target)
    {
        target.OnDeath -= ExtraBonusPoints;
        levelWinManager.AddPoints(9);

    }

    private void SuperExtraBonusPoints(IDamageable target)
    {
        target.OnDeath -= SuperExtraBonusPoints;
        levelWinManager.AddPoints(990);

    }

    IEnumerator RespawnTarget()
    {
        yield return new WaitForSeconds(2);
        SpawnTarget();
    }
}
