using UnityEngine;

public class Spawner : MonoBehaviour
{
    public SpawnData[] spawnData;

    private float time;
    private float spawnTime = 0.2f;
    private int level;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isLive) return;

        time += Time.deltaTime;
        level = (int)(GameManager.instance.gameTime / 10f);
        level = Mathf.Min(level, spawnData.Length-1);

        if (time > (level == 0 ? 0.5f : 0.3f)) 
        {
            time = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = GetRandomPositionAround(GameManager.instance.player.transform);
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }

    public Vector3 GetRandomPositionAround(Transform center, float offset = 10f)
    {
        float randomX = Random.Range(center.position.x - offset, center.position.x + offset);
        float randomY = Random.Range(center.position.y - offset, center.position.y + offset);
        float fixedZ = center.position.z;

        return new Vector3(randomX, randomY, fixedZ);
    }
}

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public int health;
    public float spawnTime;
    public float speed;
}
