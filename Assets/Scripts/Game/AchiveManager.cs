using UnityEngine;
using System;

public class AchiveManager : MonoBehaviour
{
    [SerializeField] GameObject[] lockCharacter;
    [SerializeField] GameObject[] unlockCharacter;

    enum Achive { UnLockBori };
    Achive[] achives;

    private void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));

        if(!PlayerPrefs.HasKey("MyData"))
            Init();
    }

    private void Start()
    {
        UnlockCharacter();
    }

    public void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach (Achive achive in achives)
        {
            PlayerPrefs.SetInt(achives.ToString(), 0);
        }
    }

    void UnlockCharacter()
    { 
        for(int i = 0; i < lockCharacter.Length; i++) 
        {
            string achiveName = achives[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            lockCharacter[i].SetActive(!isUnlock);
            unlockCharacter[i].SetActive(isUnlock);
        }
    }
}
