using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;
public class AdsManager : MonoBehaviour,IUnityAdsListener
{

    #if UNITY_IOS
        string gameId = "4461986";
    #else 
        string gameId = "4461987";
    #endif
    Action onRewardedAdSuccess;
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameId);
        Advertisement.AddListener(this);
        ShowBanner();
    }

    // Update is called once per frame
    public void PlayAd(){
        #if UNITY_IOS
        if(Advertisement.IsReady("Interstitial_iOS")){
            Advertisement.Show("Interstitial_iOS");
        }
        #else
        if(Advertisement.IsReady("Interstitial_Android")){
            Advertisement.Show("Interstitial_Android");
        }
        #endif

    }

    public void PlayRewardedAd(Action onSuccess){
        onRewardedAdSuccess = onSuccess;
        #if UNITY_IOS
        if(Advertisement.IsReady("Rewarded_iOS")){
            Advertisement.Show("Rewarded_iOS");
        }
        #else
        if(Advertisement.IsReady("Rewarded_Android")){
            Advertisement.Show("Rewarded_Android");
        }
        #endif    

    }

    public void ShowBanner(){
        if(Advertisement.IsReady("Banner_Android") || Advertisement.IsReady("Banner_iOS")){
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            #if UNITY_IOS
                Advertisement.Banner.Show("Banner_iOS");
            #else
                Advertisement.Banner.Show("Banner_Android");

            #endif    

        }else{
            StartCoroutine(RepeatShowBanner());
        }
    }

    IEnumerator RepeatShowBanner(){
        yield return new WaitForSeconds(1);
        ShowBanner();
    }

    public void HideBanner(){
        Advertisement.Banner.Hide(); 
    }
    public void OnUnityAdsReady(string placementId){
        Debug.Log("ads are readu");
    }
    public void OnUnityAdsDidError(string placementId){
        Debug.Log("Error msg");

    }
    public void OnUnityAdsDidStart(string placementId){
        Debug.Log("Video Started");
        
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult){
        if( (placementId == "Rewarded_Android" || placementId=="Rewarded_iOS" ) && showResult== ShowResult.Finished ){
            onRewardedAdSuccess.Invoke();
        }

    }
}