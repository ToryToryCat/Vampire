using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per; //관통 여부

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    { 
        this.damage = damage;
        this.per = per;

        if (per > -1) 
        {
            rigid.linearVelocity = dir * 15;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per < 0)
            return;

        per--;

        if (per < 0)
        {
            rigid.linearVelocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Area"))
        {
            if(rigid != null) rigid.linearVelocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
