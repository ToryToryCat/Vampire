using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;
    private SpriteRenderer player;

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 rightPosReverse = new Vector3(-0.15f, -0.15f, 0);
    Quaternion leftRot = Quaternion.Euler(0, 0, -35f);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135f);

    private void Awake()
    {
        player = GameManager.instance.player.GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        bool isReverse = player.flipX;

        if (isLeft) //근접
        {
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        else //원거리
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;
        }
    }

}
