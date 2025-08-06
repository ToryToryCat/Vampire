using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public RuntimeAnimatorController[] animCon;
    
    private Rigidbody2D target;
    private Rigidbody2D rigid;
    private Collider2D coll;

    private SpriteRenderer spriter;
    private Animator anim;

    private WaitForFixedUpdate wait;

    public float speed;
    public float health;
    public float maxHealth;

    private bool isLive;

    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();

        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);

        health = maxHealth;
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive) return;
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.linearVelocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive) return;
        if (!isLive) return;
        spriter.flipX = target.position.x < rigid.position.x;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isLive || !collision.CompareTag("Bullet")) return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());
        
        if (health > 0)
        {
            anim.SetTrigger("Hit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        }
        else
        {
            StartCoroutine(Dead());
        }
    }

    IEnumerator KnockBack()
    { 
        yield return wait; //다음 물리 프레임 딜레이

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse); //ForceMode2D 물리에 힘을 주는 방식
    }

    IEnumerator Dead()
    {
        isLive = false;
        coll.enabled = false;
        rigid.simulated = false;
        spriter.sortingOrder = 1;
        anim.SetBool("Dead", true);

        GameManager.instance.kill++;
        GameManager.instance.GetExp();

        if(GameManager.instance.isLive)
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);

        yield return new WaitForSeconds(1f);

        this.gameObject.SetActive(false);
    }
}
