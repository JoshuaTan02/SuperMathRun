using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuCtrl : MonoBehaviour
{   
    public CharacterDataBase characterDB;
    public Text nameText;
    public SpriteRenderer artworkSpirite;

    public Text factorText;
    private int factor;
    private int selectedOption = 0;

    public GameObject HomeMenu;
    public GameObject CustomizeMenu;

    public DataController DataController;

    private int highscore;

    private GameObject Addition;
    private GameObject Subtraction;
    private GameObject Multiplication;
    private GameObject Division;


    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
    public void LoadCustomizedScene(string sceneName){
        int Addition = GameObject.Find("Addition").GetComponent<Toggle>().isOn? 1:0;
        int Subtraction = GameObject.Find("Subtraction").GetComponent<Toggle>().isOn? 1:0;
        int Multiplication = GameObject.Find("Multiplication").GetComponent<Toggle>().isOn? 1:0;
        int Division = GameObject.Find("Division").GetComponent<Toggle>().isOn? 1:0;
        string factorNumber = GameObject.Find("txt_number").GetComponent<Text>().text;
        int number = int.Parse(factorNumber);
        factor = number;
        int[] settings = {Addition,Subtraction,Multiplication,Division};
        DataController.SaveCustomizedSettings(settings, number,sceneName,selectedOption);
    }

    void Start()
    {
        DataController = GameObject.Find("DATACTRL").GetComponent<DataController>();
        UpdateCharacter(selectedOption);
        GameObject.Find("txt_highscore").GetComponent<Text>().text = "Highscore: " + DataController.getHighscore();
        Debug.Log("Highscore is :" +DataController.getHighscore());
        CustomizeMenu.SetActive(false);
        
    }
    void Update()
    {
        
    }
    public void Customize(){
        HomeMenu.SetActive(false);
        CustomizeMenu.SetActive(true);
        Addition =  GameObject.Find("Addition");
        Subtraction =  GameObject.Find("Subtraction");
        Multiplication =  GameObject.Find("Multiplication");
        Division =  GameObject.Find("Division");
        
        UpdateSettings(DataController.getSettings());
        UpdateFactor(DataController.getFactorLevel());
        UpdateCharacterSelect(DataController.getCharacterIndex());
    }


    void UpdateCharacterSelect(int index){
        if(PlayerPrefs.GetInt("Character"+index) != 0){
            //If not 0 then we know the character is unlocked. 
            selectedOption = index;
            UpdateCharacter(selectedOption);

        }
    }
    public void Return(){
    HomeMenu.SetActive(true);
    CustomizeMenu.SetActive(false);
    }
    public void NextOption(){
        selectedOption++;
        if(selectedOption>=characterDB.CharacterCount ){
            selectedOption=0; 
        }
        UpdateCharacter(selectedOption);
    }
    public void BackOption(){
        selectedOption--;
        if(selectedOption<0 ){
            selectedOption= characterDB.CharacterCount-1; 
        }
        UpdateCharacter(selectedOption);
    }


    private void UpdateCharacter(int selectedOption){
        Character character = characterDB.GetCharacter(selectedOption);
        artworkSpirite.sprite = character.characterSprite;
        nameText.text = character.characterName;
    }

    private void UpdateFactor(int factor){
        Debug.Log("Factor is: " + factor);
        this.factor = factor;
        factorText.text = factor+"";

    }
    public void increaseFactor(){
        factor +=5;
        if(factor >20){
            factor = 5;
        }
        UpdateFactor(factor);
    }

    public void decreaseFactor(){
        factor -=5;
        if(factor <5){
            factor = 20;
        }
        UpdateFactor(factor);
    }

    void UpdateSettings(int[] settings){

        if(settings[0] ==1) 
        Addition.GetComponent<Toggle>().isOn = true;
        else
       Addition.GetComponent<Toggle>().isOn =false;

        if(settings[1] ==1) 
        Subtraction.GetComponent<Toggle>().isOn = true;
        else
       Subtraction.GetComponent<Toggle>().isOn =false;

        if(settings[2] ==1) 
        Multiplication.GetComponent<Toggle>().isOn = true;
        else
        Multiplication.GetComponent<Toggle>().isOn =false;

        if(settings[3] ==1) 
        Division.GetComponent<Toggle>().isOn = true;
        else
        Division.GetComponent<Toggle>().isOn =false;

        
    }

}
