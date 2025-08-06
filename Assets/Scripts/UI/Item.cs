using UnityEngine.UI;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;
    public Weapon weapon;
    public Gear gear;

    private Image icon;
    private Text textLevel;
    private Text textName;
    private Text textDesc;

    public int level;

    private void Awake()
    {
        icon = transform.Find("Icon").GetComponent<Image>();
        icon.sprite = data.itemIcon;

        textLevel = transform.Find("Text_Level").GetComponent<Text>();
        textName = transform.Find("Text_Name").GetComponent<Text>();
        textDesc = transform.Find("Text_Desc").GetComponent<Text>();

        textName.text= data.itemName;
    }

    public void OnEnable()
    {
        textLevel.text = $"Lv.{level + 1}";

        if (data.itemDesc.Contains("{1}"))
            textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100, data.count[level]);
        else if (data.itemDesc.Contains("{0}"))
            textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100);
        else    
            textDesc.text = data.itemDesc;

    }

    public void OnClikc()
    {
        GameManager.instance.uiLevelUP.Hide();

        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:

                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level - 1];
                    nextCount += data.count[level - 1];

                    weapon.LevelUp(nextDamage, nextCount);
                }

                level++;
                break;

            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:

                if (level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                { 
                    float nextRate = data.damages[level-1];
                    gear.LevelUp(nextRate);
                }

                level++;
                break;

            case ItemData.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;

            default:
                break;
        }

        if (level == data.damages.Length) 
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
