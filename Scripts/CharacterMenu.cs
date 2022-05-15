using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //text field
    public Text levelText, hitpointText, pesosText, upgradeCostText, xpText;

    //logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //Character Selection
    public void OnArrowClick(bool right)
    {
        if(right)
        {
            currentCharacterSelection++;

            //if we went too far away
            if(currentCharacterSelection == GameManager.instance.playerSprites.Count)
              currentCharacterSelection = 0;

              OnSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;

            //if we went too far away
            if(currentCharacterSelection < 0)
              currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

              OnSelectionChanged();
        }
    }
    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
    }
    //weapon upgrade
    public void OnUpgradeClick(){
        if(GameManager.instance.TryUpgradeWeapon())
          UpdateMenu();
    }
    //update the character Information
    public void UpdateMenu()
    {
        //weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if(GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
        upgradeCostText.text = "MAX";
        else
          upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        //meta
        levelText.text = "NOT IMPLEMENTED";
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();

        //Xp Bar
        xpText.text = "NOT IMPLEMENTED";
        xpBar.localScale = new Vector3(0.5f,0,0);
    }
}