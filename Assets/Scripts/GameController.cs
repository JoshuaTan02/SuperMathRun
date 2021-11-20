using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Player;
    private DataController dataController;

    private int correctAnswered;


    private int[] mathSettings;
    private int factor;

    private int score = 0;
    public Text ScoreTxt;
    
    private int correctIndex;
    private bool correctAnswer = false;
    private bool answeringQuestion = false;

    public static GameController instance;

    public Parallax BG;

    public GameObject InGameMenu;
    public GameObject HUDMenu;

    public GameObject GameOverMenu;
    public GameObject DimScreen;

    public GameObject Dog0;
    public GameObject Cat1;
    public GameObject Dino2;
    public GameObject Ninja3;

    public AdsManager ads;
    void Start()
    {
        dataController = GameObject.Find("DATACTRL").GetComponent<DataController>();
        RenderCharacter(new Vector3(0,-2.5f,0.56f));
        Player= GameObject.FindWithTag("Player");
        mathSettings = dataController.getSettings();
        factor  = dataController.getFactorLevel();
        BG = GameObject.Find("BG_1").GetComponent<Parallax>();
        HUDMenu.SetActive(false);
        Continue();
        //Smth not working here
        //UpdateProblem();
        
        
    }
    void Awake()
    {
        if(instance==null)
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        
        if(!Player.GetComponent<PlayerCtrl>().isAlive){
            GameOver();

        }
        if(Input.GetKeyDown(KeyCode.A)){
            UpdateProblem();
                float xOffset = 12;
                float yOffset =2.0f;
                SFXCtrl.instance.ShowAnswerSparkle(new Vector2(Player.GetComponent<Transform>().position.x + xOffset,Player.GetComponent<Transform>().position.y + yOffset ));

        }
        if(Input.GetKeyDown(KeyCode.B)){
            ButtonClickSFX();
        }
        score = (int)Player.GetComponent<Transform>().position.x;
        ScoreTxt.text = "SCORE: " + score;
    }

    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    public string[] createProblem(){
        int randomIndex = Random.Range(0,mathSettings.Length);
        correctAnswer = false;
        while(mathSettings[randomIndex] != 1){
            randomIndex = Random.Range(0,mathSettings.Length);
        }
        Hashtable answers = new Hashtable();
        int first;
        int second;
        int answer;
        string question="";
        int possibleAnswers = 0;
        int possibleAnswer = 0;
        float diviPossibleAnswer = 0.0f;
        //answer will always be the zero index
        switch (randomIndex){
            
            case 0:
            //Create addition problem
            first = Random.Range(0,factor+1);
            second = Random.Range(0,factor+1);
            question = first + " + " + second;

            answer= first+second;
            answers.Add(question,answer);
            //Create three other answers that are not hte same and will be with the key from 0-2
            while(possibleAnswers<3){
                possibleAnswer = Random.Range(0,factor+1) + Random.Range(0,factor+1);

                if(!answers.ContainsValue(possibleAnswer)){
                    answers.Add(""+possibleAnswers, possibleAnswer);
                    possibleAnswers++;
                }

            }

            break;
            case 1:
            //Create subtraction problem
            first = Random.Range(0,factor+1);
            second = Random.Range(0,factor+1);
            question = first + " - " + second;
            answer = first-second;
            answers.Add(question,answer);
            //Create three other answers that are not hte same and will be with the key from 0-2
            while(possibleAnswers<3){
                possibleAnswer = Random.Range(0,factor+1) - Random.Range(0,factor+1);

                if(!answers.ContainsValue(possibleAnswer)){
                    answers.Add(""+possibleAnswers, possibleAnswer);
                    possibleAnswers++;
                    
                }

            }
            break;
            case 3:
            //Create division problem
            first = Random.Range(0,factor+1);            
            second = Random.Range(0,factor+1);
            while(second ==0){
                second = Random.Range(0,factor+1);
            }
            question = first + " ÷ "+ second;
            Debug.Log("Question is: " + question);
            float diviAnswer = (float)first / (float)second ;
            diviAnswer = Mathf.Round(diviAnswer * 100.0f) * 0.01f;

            answers.Add(question,diviAnswer);
            answers.Add("Division",true);
            //Create three other answers that are not hte same and will be with the key from 0-2
            while(possibleAnswers<3){
                second = Random.Range(0,factor+1);
                while(second ==0){
                    second = Random.Range(0,factor+1);
                }
                diviPossibleAnswer = (float)Random.Range(0,factor+1) / (float)second;
                diviPossibleAnswer = Mathf.Round(diviPossibleAnswer * 100.0f) * 0.01f;
                if(!answers.ContainsValue(diviPossibleAnswer)){
                    answers.Add(""+possibleAnswers, diviPossibleAnswer);
                    possibleAnswers++;
                }

            }
            break;
            case 2:
            //Create multi problem
            first = Random.Range(0,factor+1);
            second = Random.Range(0,factor+1);
            question = first + " X " + second;
            answer = first*second;
            answers.Add(question,answer);
            //Create three other answers that are not hte same and will be with the key from 0-2
            while(possibleAnswers<3){
                possibleAnswer = Random.Range(0,factor+1) * Random.Range(0,factor+1);

                if(!answers.ContainsValue(possibleAnswer)){
                    answers.Add(""+possibleAnswers, possibleAnswer);
                    possibleAnswers++;
                }

            }
            break;
            
        }

        string[] results = {question, answers[question]+"",answers[0+""]+"",answers[1+""]+"",answers[2+""]+"" };

        return results;
    }

    public void UpdateProblem(){
        string[] results = createProblem();
        answeringQuestion = true;
        correctIndex = Random.Range(1,results.Length);
        GameObject.Find("txt_choice"+(correctIndex)).GetComponent<Text>().text = results[1];


        GameObject.Find("txt_problem").GetComponent<Text>().text = results[0];
        
        for(int i = 1; i <= 4; i++){
            if(i < correctIndex){
                //put the wrong answer in the buttn choice
                GameObject.Find("txt_choice"+(i)).GetComponent<Text>().text = results[i+1];
            }
            if(i > correctIndex){
                GameObject.Find("txt_choice"+(i)).GetComponent<Text>().text = results[i];
            }
        }
    }
    public void GameOver(){
        //The player died. Now show the game over screen and give chance for extra live. 

        BG.speed =false;
        Time.timeScale = 0;
        GameOverMenu.SetActive(true);
        DimScreen.SetActive(true);
        SFXCtrl.instance.changeVolumeBG(.2f);

    }

    public void Revive(){
        ads.PlayRewardedAd(RewardedAds);
    }
    public void RewardedAds(){
        //Unpause the game and have the playyer reset position but keep everythign else
        
        int offset = 15-(score%30); 
        Player.GetComponent<Transform>().position = new Vector3(Player.GetComponent<Transform>().position.x+ offset,Player.GetComponent<Transform>().position.y + 1.5f,Player.GetComponent<Transform>().position.z);
        Player.GetComponent<PlayerCtrl>().isAlive = true;
        Continue();

        UpdateProblem();

    }
    public void GameOverChoice(string sceneName){
        //Player decided to retry or go main menu. so update the score if it changed. 
        dataController.updateHighscore(score,correctAnswered);
        bool playAd = dataController.AddDeath();
        if(sceneName.Equals("menu")){
            ads.PlayAd();    
        }
        Continue();
        Player.GetComponent<PlayerCtrl>().isAlive = true;
        LoadScene(sceneName);

    }

    public void PauseGame(){
        BG.speed =false;
        Time.timeScale = 0;
        InGameMenu.SetActive(false);
        HUDMenu.SetActive(true);

    }

    public void Restart(){

        Continue();
        LoadScene("Gameplay");
    }
    public void Continue(){
        BG.speed =true;
        Time.timeScale = 1;
        InGameMenu.SetActive(true);
        HUDMenu.SetActive(false);
        DimScreen.SetActive(false);
        GameOverMenu.SetActive(false);
        SFXCtrl.instance.changeVolumeBG(.7f);
    }


    public void SelectAnswer(int index){
        if(answeringQuestion){
            if(index == correctIndex){
                //They selected the right answer

                correctAnswer = true;
                float xOffset = 12;
                float yOffset =2.0f;
                SFXCtrl.instance.ShowAnswerSparkle(new Vector2(Player.GetComponent<Transform>().position.x + xOffset,Player.GetComponent<Transform>().position.y + yOffset ));
                SFXCtrl.instance.PlayCorrectAnswer();
                correctAnswered++;
            }else{
                //They selected the wrong answer 
                SFXCtrl.instance.PlayWrongAnswer();
            }
        }

        answeringQuestion = false;
        EmptyQuestions();
        correctIndex = -1;
    }
    public bool isCorrectAnswer(){
        return correctAnswer;
    }
    void EmptyQuestions(){

        GameObject.Find("txt_problem").GetComponent<Text>().text = "";
    
        for(int i = 1; i <= 4; i++){
                //Emptying the choices
                GameObject.Find("txt_choice"+(i)).GetComponent<Text>().text = "";
        }
    }

    void ButtonClickSFX(){
        SFXCtrl.instance.PlayButtonClick();
    }
    
    void RenderCharacter(Vector3 pos){
        int characterIndex = dataController.getCharacterIndex();
        switch (characterIndex){
            case 0:
            Instantiate(Dog0,pos ,Quaternion.identity);
            break;
            case 1:
            Instantiate(Cat1,pos ,Quaternion.identity);
            break;
            case 2:
            Instantiate(Dino2,pos ,Quaternion.identity);
            break;
            case 3:
            Instantiate(Ninja3,pos ,Quaternion.identity);
            break;
        }


    }

}
