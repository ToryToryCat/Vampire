using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Player player;
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float baseSpeed;
    public float speed;

    private float timer;

    void Awake()
    {
        player = GameManager.instance.player;
    }

    void Update()
    {
        if (!GameManager.instance.isLive) return;

        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            default:

                timer += Time.deltaTime;

                if(timer > speed)
                {
                    timer = 0;
                    Fire();
                }

                break;
        }
    }

    public void LevelUp(float damage, int count)
    { 
        this.damage = damage;
        this.count = count;

        if (id == 0)
            Batch();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        // Basic Set
        name = $"Weapon {data.itemId}";
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;
        baseSpeed = data.baseSpeed;

        for (int i = 0; i < GameManager.instance.pool.prefabs.Length; i++)
        {
            if (data.projectile == GameManager.instance.pool.prefabs[i])
            {
                prefabId = i;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = baseSpeed * Character.WeaponSpeed;
                Batch();
                break;

            case 1:
                speed = baseSpeed * Character.WeaponSpeed;
                break;

            default:
                break;
        }

        // Hand Set
        Hand hand = player.Hands[(int)data.itemType];
        hand.spriter.sprite = data.hand;
        hand.gameObject.SetActive(true);

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Batch()
    {
        for(int i = 0; i < count; i++) 
        {
            Transform bullet = null;

            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            { 
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = (Vector3.forward * 360 * i) / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 무한 관통
        }
    }

    void Fire()
    {
        if (player.scanner.nearestTarget != null)
        {
            Vector3 targetPos = player.scanner.nearestTarget.position;
            Vector3 dir = targetPos - transform.position;
            dir = dir.normalized;

            Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.GetComponent<Bullet>().Init(damage, count, dir);

            AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
        }
    }
}
