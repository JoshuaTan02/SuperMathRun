using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXCtrl : MonoBehaviour
{
    /*


    Audio clips index:
    
    0: correct answer
    1: wrong answer
    2: button clip
    3: running noise
    4: jump sfx
    5: hit obstacle
    6: BG Music




    */
    public static SFXCtrl instance;
    public GameObject sfx_answer;

    public AudioSource audioSource;

    public AudioSource BGMusic;

    public AudioClip[] audioClips;

    void Awake()
    {
        if(instance==null)
        instance = this;

    }
    void Start(){
        BGMusic.loop = true;
    }
    // Start is called before the first frame update


    //When the player gets the right answer
    public void ShowAnswerSparkle(Vector2 pos){
        Instantiate(sfx_answer,pos,Quaternion.identity);
    }

    public void PlayCorrectAnswer(){
        playAudio(getClip(0));
    }

    public void PlayWrongAnswer(){
        playAudio(getClip(1));

    }

    public void PlayButtonClick(){
        playAudio(getClip(2));

    }    

    public void PlayRunning(){
        playAudio(getClip(3));

    }    
    public void PlayJumpSFX(){
        playAudio(getClip(4));

    }
    public void PlayObstacleHit(){
        playAudio(getClip(5));

    }
    public void PlayBGMusic(){
        BGMusic.loop= true;
        BGMusic.PlayOneShot(getClip(6));

    }    


    
    private void playAudio(AudioClip clip){
        audioSource.PlayOneShot(clip);
    }
    private AudioClip getClip(int index){
        return audioClips[index];
    }
    
    }

