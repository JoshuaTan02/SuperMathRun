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
        Debug.Log("Getting settings");
        correctAnswered = PlayerPrefs.GetInt(CORRECTANSWERED,0);
        highscore = PlayerPrefs.GetInt(HIGHSCORE,0);
        factorlevel = PlayerPrefs.GetInt(FACTORLEVEL,5);
        settings[0] = PlayerPrefs.GetInt("0", 1);
        characters[0] = PlayerPrefs.GetInt("Character0", 1);
        characterSelectIndex = PlayerPrefs.GetInt(CHARACTERINDEX,0);
        for(int i =1; i < settings.Length; i++){
            settings[i] = PlayerPrefs.GetInt(i+"", 0);
        }
        for(int i =1; i < characters.Length; i++){
            characters[i] = PlayerPrefs.GetInt("Character" + i, 0);
        }

        checkAchievements(1000);
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
        this.characterSelectIndex = characterindex;
        PlayerPrefs.SetInt(CHARACTERINDEX,characterindex);
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

    public void updateHighscore(int newScore){
        if(newScore > highscore){
            highscore = newScore;
            PlayerPrefs.SetInt(HIGHSCORE,highscore);
            Debug.Log("Player set new high score");
        //If new highscore then should check if got any acheivements
            checkAchievements(highscore);

        }
    }

    public void upddateCorrectAnswers(int num){
        correctAnswered+=num;
        PlayerPrefs.SetInt(CORRECTANSWERED,correctAnswered);

    }
    void checkAchievements(int score){
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
}
