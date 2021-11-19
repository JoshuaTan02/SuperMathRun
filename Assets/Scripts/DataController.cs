using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour
{
    //Character0 is dog 
    //Character1 is cat
    //Character2 is dino
    //Character3 is ninja


    private int highscore;
    private string HIGHSCORE = "highscore";
    private string FACTORLEVEL = "factorlevel";
    private string CHARACTERINDEX= "CharacterIndex";

    private string CORRECTANSWERED = "CorrectAnswered";
    private int factorlevel;

    private int correctAnswered;

    private int characterSelectIndex= 0;
    private int[] settings = new int[4];

    private int[] characters = new int[4];
    void Awake(){
        int numControllers=FindObjectsOfType<DataController>().Length;
        if(numControllers!=1){
            Destroy(this.gameObject);
        }else{
        DontDestroyOnLoad(this);

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.DeleteAll();
        correctAnswered = PlayerPrefs.GetInt(CORRECTANSWERED,0);
        highscore = PlayerPrefs.GetInt(HIGHSCORE,0);
        factorlevel = PlayerPrefs.GetInt(FACTORLEVEL,5);
        PlayerPrefs.SetInt(FACTORLEVEL,factorlevel);
        settings[0] = PlayerPrefs.GetInt("0", 1);
        characters[0] = 1;
        PlayerPrefs.SetInt("Character0", 1);
        characterSelectIndex = PlayerPrefs.GetInt(CHARACTERINDEX,0);
        for(int i =1; i < settings.Length; i++){
            settings[i] = PlayerPrefs.GetInt(i+"", 0);
        }
        for(int i =1; i < characters.Length; i++){
            characters[i] = PlayerPrefs.GetInt("Character" + i, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveCustomizedSettings(int[] newSettings, int factorlevel, string sceneName, int characterindex){

        bool nosettings = true;
        Debug.Log("Saving Customized Settings");

        for (int i =0; i < newSettings.Length; i++){
            if (newSettings[i] ==1)
                nosettings=false;

            PlayerPrefs.SetInt(i+"", newSettings[i]);
        }
        settings = newSettings;

        if(nosettings){
            PlayerPrefs.SetInt("0",1);
            settings[0] = PlayerPrefs.GetInt("0", 1);
        }
        this.factorlevel = factorlevel; 
        if(isUnlocked(characterindex)){
            //User selected a character that is unlocked
            Debug.Log("character is unlocked");

        this.characterSelectIndex = characterindex;
        PlayerPrefs.SetInt(CHARACTERINDEX,characterindex);
        }else{
            Debug.Log("character is not unlocked");
        }

        PlayerPrefs.SetInt(FACTORLEVEL, factorlevel);
        SceneManager.LoadScene(sceneName);

    }
    
    public int[] getSettings(){
        return settings;

    }

    public int getCharacterIndex(){
        return PlayerPrefs.GetInt(CHARACTERINDEX);
    }
    public int getFactorLevel(){
        return PlayerPrefs.GetInt(FACTORLEVEL);
    }

    public void updateHighscore(int newScore, int numCorrectAnswers){
        if(newScore > highscore){
            highscore = newScore;
            PlayerPrefs.SetInt(HIGHSCORE,highscore);
            Debug.Log("Player set new high score");
        //If new highscore then should check if got any acheivements
            checkAchievements(highscore, numCorrectAnswers);

        }
    }

    public void upddateCorrectAnswers(int num){
        correctAnswered+=num;
        PlayerPrefs.SetInt(CORRECTANSWERED,correctAnswered);

    }
    void checkAchievements(int score, int correctAnswers){
        //unlock 2nd character by getting score of 100
        if(PlayerPrefs.GetInt("Character"+1) ==0 && score>99){
            //we know not unlocked yet 
            PlayerPrefs.SetInt("Character"+1,1);
            characters[1] = 1;
            Debug.Log("Unlocked character 2");
        }
    }
    public int getHighscore(){
        Debug.Log("Answered correct is: " + PlayerPrefs.GetInt(CORRECTANSWERED));
        return PlayerPrefs.GetInt(HIGHSCORE);
    }

    public bool isUnlocked(int index){
        Debug.Log("character at index: " + index + "is: "  + PlayerPrefs.GetInt("Character" + index));
        return PlayerPrefs.GetInt("Character" + index)!=0;
    }
}
