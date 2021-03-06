using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake(){
       if(GameManager.instance != null)
       {
           Destroy(gameObject);
           return;
       }

       instance = this;
       
       SceneManager.sceneLoaded += LoadState;
       DontDestroyOnLoad(gameObject);
    }

    //ressources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;

    //logic
    public int pesos;
    public int experience;

    //floating text
    public void ShowText(string msg, int fontSize,Color color,Vector3 position,Vector3 motion,float duration)
    {
       floatingTextManager.Show(msg,fontSize,color,position,motion,duration);
    }
    //Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
       // is the weapon max level?
       if(weaponPrices.Count <= weapon.weaponLevel)
       return false;

       if(pesos >= weaponPrices[weapon.weaponLevel])
       {
          pesos -= weaponPrices[weapon.weaponLevel];
          weapon.UpgradeWeapon();
          return true;
       }

       return false;
    }

    public void SaveState()
    {
        
       string s ="";
       s += "0" + "|";
       s += pesos.ToString() + "|";
       s += experience.ToString() + "|";
       s += weapon.weaponLevel.ToString();
    }
    
    public void LoadState(Scene s, LoadSceneMode mode)
    {
       if(!PlayerPrefs.HasKey("SaveState"))
       return;

       string[] data = PlayerPrefs.GetString("SaveState").Split('|');
       //change player skin
       pesos = int.Parse(data[1]);
       experience = int.Parse(data[2]);
       //change the weapon level
       weapon.SetWeaponLevel (int.Parse(data[3]));
       
       Debug.Log("LoadState");
    }
}
