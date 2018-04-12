using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppsflyerAnalyticsHandler : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        //Mandatory - set your AppsFlyer’s Developer key.
        AppsFlyer.setAppsFlyerKey("ZmysHsNw9VtBeDmP4maYab"); // 
        // For detailed logging
        //AppsFlyer.setIsDebug (true);
#if UNITY_IOS
            //Mandatory - set your apple app ID
               AppsFlyer.setAppID ("YOUR_APP_ID_HERE");
              AppsFlyer.trackAppLaunch ();
#elif UNITY_ANDROID
        //Mandatory - set your Android package name
        AppsFlyer.setAppID(Application.bundleIdentifier);
        AppsFlyer.init("ZmysHsNw9VtBeDmP4maYab");
        AppsFlyer.setGCMProjectNumber("60588766610");
#endif
        //For getting the conversion data in Android, you need to this listener.
        AppsFlyer.loadConversionData(name, "didReceiveConversionData", "didReceiveConversionDataWithError");
        AppsFlyer.createValidateInAppListener(name, "onInAppBillingSuccess", "onInAppBillingFailure");

        string[] keys;
        string[] values;

        if (!PlayerPrefs.HasKey("AppsFlyAnalytics"))
        {
            PlayerPrefs.SetInt("AppsFlyAnalytics", 1);
            keys = new string[] { "status" };
            values = new string[] { "new" };
            TrackEvent("NewUser", keys, values);
        }
    }
    public void didReceiveConversionData(string conversionData)
    {
        print("AppsFlyerTrackerCallbacks:: got conversion data = " + conversionData);
    }

    public void didReceiveConversionDataWithError(string error)
    {
        print("AppsFlyerTrackerCallbacks:: got conversion data error = " + error);
    }

    public void didFinishValidateReceipt(string validateResult)
    {
        print("AppsFlyerTrackerCallbacks:: got didFinishValidateReceipt  = " + validateResult);

    }

    public void didFinishValidateReceiptWithError(string error)
    {
        print("AppsFlyerTrackerCallbacks:: got idFinishValidateReceiptWithError error = " + error);

    }

    public void onAppOpenAttribution(string validateResult)
    {
        print("AppsFlyerTrackerCallbacks:: got onAppOpenAttribution  = " + validateResult);

    }

    public void onAppOpenAttributionFailure(string error)
    {
        print("AppsFlyerTrackerCallbacks:: got onAppOpenAttributionFailure error = " + error);

    }

    public void onInAppBillingSuccess()
    {
        print("AppsFlyerTrackerCallbacks:: got onInAppBillingSuccess succcess");

    }
    public void onInAppBillingFailure(string error)
    {
        print("AppsFlyerTrackerCallbacks:: got onInAppBillingFailure error = " + error);

    }

    public static void TrackEvent(string EventName, string[] keys, string[] values)
    {
        Dictionary<string, string> currentEventData = new Dictionary<string, string>();
        for (int i = 0; i < keys.Length; i++)
        {
            currentEventData.Add(keys[i], values[i]);
        }
#if UNITY_EDITOR
        //Debug.LogError("EventName :: " + EventName);
#elif UNITY_ANDROID
         AppsFlyer.trackRichEvent(EventName, currentEventData);
#endif
    }

    public static void TrackEvent(string EventName, Dictionary<string, string> currentEventData)
    {
#if UNITY_EDITOR
        //Debug.LogError("EventName :: " + EventName);
#elif UNITY_ANDROID
         AppsFlyer.trackRichEvent(EventName, currentEventData);
#endif
    }
}
