using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    public Image itemImage;
    public Text itemName;
    public Text itemAtkValue;
    public Text itemDefValue;

    public GameObject SkillUI;
    public Image skillImage1;
    public Image skillImage2;
    public Text skillName1, skillName2;
    public Text skillInfo1, skillInfo2;

    public void UpdateUI(Equipment item)
    {
        itemImage.sprite = item.icon;
        itemName.text = item.name;
        itemAtkValue.text = item.damageModifier.ToString();
        itemDefValue.text = item.armorModifier.ToString();

        if (item.equipSlot == EquipmentSlot.Weapon) {
            SkillUI.SetActive(true);
            EquipmentWeapon equipmentWeapon = item as EquipmentWeapon;
            skillImage1.sprite = equipmentWeapon.skill[0].icon;
            skillName1.text = equipmentWeapon.skill[0].name;
            skillInfo1.text = equipmentWeapon.skill[0].description;

            skillImage2.sprite = equipmentWeapon.skill[1].icon;
            skillName2.text = equipmentWeapon.skill[1].name;
            skillInfo2.text = equipmentWeapon.skill[1].description;
        }
        else
        {
            SkillUI.SetActive(false);
        }
    }
}
