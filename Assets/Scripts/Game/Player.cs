using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;

    private Rigidbody2D rigid;
    private SpriteRenderer spriter;
    private Animator anim;
    public Scanner scanner;
    public Hand[] Hands;
    public RuntimeAnimatorController[] animCon;

    public float speed;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        Hands = GetComponentsInChildren<Hand>(true);
    }

    public void OnEnable()
    {
        speed *= Character.Speed;
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive) return;

        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec); //위치 이동
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();

        anim.SetFloat("Speed", inputVec.sqrMagnitude);

        if (inputVec.x != 0)
            spriter.flipX = inputVec.x < 0;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive) return;

        GameManager.instance.health -= 10 * Time.deltaTime;

        if (GameManager.instance.health <= 0)
        {
            for(int i = 0; i < Hands.Length; i++) 
            {
                Hands[i].gameObject.SetActive(false);
            }

            anim.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }
}
