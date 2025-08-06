using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        // Basic Set
        name = $"Gear {data.itemId}";
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        type = data.itemType;
        rate = data.baseDamage;

        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons) 
        {
            switch (weapon.id)
            {
                case 0:
                    weapon.speed = weapon.baseSpeed + (weapon.baseSpeed * rate);
                    break;

                case 1:
                    weapon.speed = weapon.baseSpeed * (1f - rate);
                    break;
            }
        }
    }

    void SpeedUp()
    {
        GameManager.instance.player.speed = rate * Character.Speed;
    }
}
