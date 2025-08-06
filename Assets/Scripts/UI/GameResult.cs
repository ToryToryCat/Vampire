using UnityEngine;

public class GameResult : MonoBehaviour
{
    [SerializeField] GameObject[] titles;

    public void Lose()
    {
        titles[0].SetActive(true);
    }

    public void Win() 
    {
        titles[1].SetActive(true);
    }
}
