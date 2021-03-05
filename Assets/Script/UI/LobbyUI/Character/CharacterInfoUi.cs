using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class CharacterInfoUI : MonoBehaviour
{
    private bool active = false;

    public Sprite emptyWeapon;
    public Sprite emptyArmor;

    public Text cha_Level;
    public Image cha_Image;
    public Text class_txt;
    public Text maxHP_txt;
    public Text def_txt;
    public Text atk_txt;

    public Slot equip_Weapon;
    public Slot equip_Armor;

    private void Start()
    {
        SetInfo();
    }

    public void SetInfo()
    {
        int index = CharacterManager.instance.playerIndex;

        cha_Level.text = CharacterManager.instance.stats_List[index].level.ToString();
        cha_Image.sprite = CharacterManager.instance.stats_List[index].icon;
        class_txt.text = CharacterManager.instance.stats_List[index].job;
        maxHP_txt.text = CharacterManager.instance.stats_List[index].maxHealth.ToString();
        def_txt.text = CharacterManager.instance.stats_List[index].CurrentArmor.ToString();
        atk_txt.text = CharacterManager.instance.stats_List[index].CurrentDamage.ToString();

        if (CharacterManager.instance.stats_List[index].weapon != null)
            equip_Weapon.icon.sprite = CharacterManager.instance.stats_List[index].weapon.icon;
        else
        {
            equip_Weapon.icon.sprite = emptyWeapon;
        }
        if (CharacterManager.instance.stats_List[index].armor != null)
            equip_Armor.icon.sprite = CharacterManager.instance.stats_List[index].armor.icon;
        else
        {
            equip_Armor.icon.sprite = emptyArmor; ;
        }
    }

    public void ClickInfo()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }

    public void SwapInfo(string index)
    {
        CharacterManager.instance.playerIndex = int.Parse(index);
        SetInfo();
    }

    public void Clickcharacter()  // OnClick~~~로 바꾸기
    {
        active = !active;
        this.gameObject.SetActive(active);
    }
}
