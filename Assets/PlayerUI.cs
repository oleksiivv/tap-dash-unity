﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;
using GoogleMobileAds.Api;

public class PlayerUI : ScenesManager
{
    public GameObject pausePanel;
    public GameObject deathPanel;

    public GameObject winPanel;

    public static int addCnt=1;

#if UNITY_IOS
    private string appId="4234608";
#else
    private string appId="4234609";
#endif

    private bool adsAlreadyShowed=false;

    public void Awake(){

        Time.timeScale=1;
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetSameAppKeyEnabled(true).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => {
            LoadLoadInterstitialAd();

            CreateBannerView();
            LoadBannerAd();
        });
        
        Advertisement.Initialize(appId,false);

        adsAlreadyShowed=false;
        

        canMove=true;
    }

    public static bool canMove=true;

    public void pause(){
        Time.timeScale=0;
        pausePanel.SetActive(true);

        canMove=false;
        if(addCnt%2==0){
            //if(Advertisement.IsReady()){
            adsAlreadyShowed=true;
            Advertisement.Show("Interstitial_Android");
            //}
            
        }
        addCnt++;
    }
    public void resume(){
        Time.timeScale=1;
        pausePanel.SetActive(false);

        Invoke(nameof(resetCanMove), 0.2f);
    }

    void resetCanMove(){
        canMove=true;
    }
    
    public void restart(){
        openScene(Application.loadedLevel);
    }
    public void next(){
        openScene(Application.loadedLevel+1);
    }

    public void showDeathPanel(){
        deathPanel.SetActive(true);
        if(addCnt%2==0){
            if(!showIntersitionalAd()){
                //if(Advertisement.IsReady()){
                adsAlreadyShowed=true;
                Advertisement.Show("Interstitial_Android");
                //}
            } else {
                adsAlreadyShowed=true;
            }
            
        }
        addCnt++;
    }
    public void showWinPanel(){
        winPanel.SetActive(true);
        if(addCnt%2==0){
            if(!showIntersitionalAd()){
                //if(Advertisement.IsReady()){
                adsAlreadyShowed=true;
                Advertisement.Show("Interstitial_Android");
                //}
            } else {
                adsAlreadyShowed=true;
            }
            
        }
        addCnt++;
    }


#if UNITY_IOS
    private string appId_admob="ca-app-pub-4962234576866611~7942157909";
    private string intersitionalId="ca-app-pub-4962234576866611/7750586215";

    private string bannerId = "ca-app-pub-4962234576866611/1376749551";
#else
    private string appId_admob="ca-app-pub-4962234576866611~9475644858";
    private string intersitionalId="ca-app-pub-4962234576866611/6236427362";

    private string bannerId = "ca-app-pub-4962234576866611/7189082196";
#endif

     AdRequest AdRequestBuild(){
         return new AdRequest.Builder().Build();
     }


      public bool showIntersitionalAd(){
        if(adsAlreadyShowed)return true;

        return showIntersitionalGoogleAd();
      }

    private InterstitialAd _interstitialAd;

    private BannerView _bannerView;
    
    public void LoadLoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
                _interstitialAd.Destroy();
                _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(intersitionalId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                    "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                            + ad.GetResponseInfo());

                _interstitialAd = ad;
            });
    }


      public bool showIntersitionalGoogleAd(){
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            _interstitialAd.Show();

            return true;
        }
        else
        {
            return false;
        }
      }

    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyBannerView();
        }

        // Create a 320x50 banner at top of the screen
        _bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.BottomRight);
    }

    public void LoadBannerAd()
    {
        // create an instance of a banner view first.
        if(_bannerView == null)
        {
            CreateBannerView();
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest);
    }

    public void DestroyBannerView()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner view.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }

    AdRequest AdRequestBannerBuild(){
        return new AdRequest.Builder().Build();
    }
}

