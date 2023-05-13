using IWantToWorkAtComplexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private ReturnToPoolOnDamage targetPrefab;
    [SerializeField] private float minimumSpawnDistance;
    [SerializeField] private float maximumSpawnDistance;

    private LevelWinManager levelWinManager;
    private GameObject player;
    private Pool<ReturnToPoolOnDamage> targetPool;
    // Start is called before the first frame update
    void Start()
    {
        levelWinManager = FindFirstObjectByType<LevelWinManager>();
        player = GameObject.FindWithTag("Player");
        targetPool = LazyPoolerUtility.GetSimplePool<ReturnToPoolOnDamage>(targetPrefab.gameObject);



        for (int i = 0; i < 25; i++)
        {
            SpawnTarget();
        }

    }

    private void SpawnTarget()
    {
        ReturnToPoolOnDamage newTarget = LazyPoolerUtility.GetSimplePooledObject<ReturnToPoolOnDamage>(targetPrefab.gameObject);

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
        newTarget.OnDamage += NewTarget_OnDamage;
    }

    private void NewTarget_OnDamage(ReturnToPoolOnDamage target)
    {
        target.OnDamage -= NewTarget_OnDamage;
        targetPool.Release(target);
        levelWinManager.AddPoints(1);
        StartCoroutine(RespawnTarget());
    }

    IEnumerator RespawnTarget()
    {
        yield return new WaitForSeconds(2);
        SpawnTarget();
    }
}
