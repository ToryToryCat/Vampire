using UnityEngine;

public class Repostion : MonoBehaviour
{
    private Collider2D coll;
    public float tileSize = 40f;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || GameManager.instance.player == null) return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;

        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = playerPos - myPos;

        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":

                if (diffX >= diffY)
                    transform.Translate(Vector3.right * dirX * tileSize);
                else
                    transform.Translate(Vector3.up * dirY * tileSize);

                break;
            
            case "Enemy":

                if(coll.enabled) 
                {
                    Vector2 ranPos = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
                    transform.Translate(GameManager.instance.player.inputVec * 20 + ranPos);
                }

                break;
        }
    }
}
