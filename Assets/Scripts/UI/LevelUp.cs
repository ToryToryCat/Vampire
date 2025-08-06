using UnityEngine;
using System.Linq;

public class LevelUp : MonoBehaviour
{
    private RectTransform rect;
    Item[] items;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);
    }

    public void Select(int index)
    {
        items[index].OnClikc();
    }

    public void Next()
    {
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        int[] ran = Enumerable.Range(0, items.Length).OrderBy(x => Random.value).Take(3).ToArray();

        for(int i = 0; i < ran.Length; i++)
        {
            Item ranItem = items[ran[i]];

            //만렙 아이템일 경우 소비 아이템으로 변경
            if (ranItem.level == ranItem.data.damages.Length)
                items[4].gameObject.SetActive(true);
            else
                ranItem.gameObject.SetActive(true);
        }
    }
}
