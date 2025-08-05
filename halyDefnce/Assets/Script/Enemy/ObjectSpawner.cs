using UnityEngine;
using UnityEngine.Pool;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Prefab and Pooling")]
    public GameObject objectPrefab;
    private ObjectPool<GameObject> objectPool;

    [Header("Spawn Area")]
    public Vector2 areaSize = new Vector2(10f, 10f);
    public Transform areaCenter;

    [Header("Spawn Settings")]
    public float spawnInterval = 2f;
    private float timer;

    private void Start()
    {
        objectPool = new ObjectPool<GameObject>(
            CreateBullet,
            actionOnGet: obj => obj.SetActive(true),
            actionOnRelease: obj => obj.SetActive(false),
            actionOnDestroy: obj => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 100
        );
    }
    private GameObject CreateBullet()
    {
        GameObject metoer = Instantiate(objectPrefab,gameObject.transform);
        metoer.GetComponent<Meteor>().SetPool(objectPool);
        return metoer;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnRandomObject();
            timer = 0f;
        }
    }

    void SpawnRandomObject()
    {
        Vector3 randomPos = GetRandomPositionInArea();
        GameObject obj = objectPool.Get();
        obj.transform.position = randomPos;

     
    }

    Vector3 GetRandomPositionInArea()
    {
        Vector2 randomOffset = new Vector2(
            Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
            Random.Range(-areaSize.y / 2f, areaSize.y / 2f)
        );

        Vector3 position = areaCenter != null ? areaCenter.position : Vector3.zero;
        return position + new Vector3(randomOffset.x, 0f, randomOffset.y); // در محور X و Z
    }

 

    private void OnDrawGizmosSelected()
    {
        if (areaCenter == null)
            return;

        Gizmos.color = Color.cyan;

        Vector3 center = areaCenter.position;
        Vector3 size = new Vector3(areaSize.x, 0.1f, areaSize.y); 

        Gizmos.DrawWireCube(center, size);
    }

}