using EntertainmentFramework;
using EntertainmentFramework.InternalLoggers;
using EntertainmentFramework.Utilities;
using EntertainmentFramework.Utils;
using EntertainmentFramework.Utils.Rest;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DemoAPIManager : MonoBehaviour
{
    [SerializeField] private Text jsonLogger;
   
    /// <summary>
    /// Making an API call for get Random Activity
    /// </summary>
    /// <param name="onCompletion"></param>
    /// <param name="onError"></param>
    private void RandomActivity(Action<string> onCompletion,
           Action<RestUtil.RestCallError> onError)
    {
        var builder = new WebRequestBuilder()
            .Url(RestManager.GetApiUrl(Urls.RANDOM_ACTIVITY))
            .Verb(Verbs.GET)
            .ContentType(ContentTypes.JSON);
       
      //  RestManager.SendWebRequest(builder, onCompletion, onError);
    }

    /// <summary>
    /// Making an API call for getting Activity by Key
    /// </summary>
    /// <param name="keyId"></param>
    /// <param name="onCompletion"></param>
    /// <param name="onError"></param>
    private void ActivityByKey(string keyId, Action<string> onCompletion,
          Action<RestUtil.RestCallError> onError)
    {        
        var builder = new WebRequestBuilder()
            .Url(RestManager.FormatApiUrl(Urls.ACTIVITY_BY_KEY,keyId))
            .Verb(Verbs.GET)
            .ContentType(ContentTypes.JSON);
           

      //  RestManager.SendWebRequest(builder, onCompletion, onError);
    }

    /// <summary>
    ///  Making an API call for getting Activity by Type
    /// </summary>
    /// <param name="typeID"></param>
    /// <param name="onCompletion"></param>
    /// <param name="onError"></param>
    private void ActivityByType(string typeID, Action<string> onCompletion,
          Action<RestUtil.RestCallError> onError)
    {
        var builder = new WebRequestBuilder()
             .Url(RestManager.FormatApiUrl(Urls.ACTIVITY_BY_TYPE, typeID))
             .Verb(Verbs.GET)
             .ContentType(ContentTypes.JSON);

      //  RestManager.SendWebRequest(builder, onCompletion, onError);
    }

    /// <summary>
    ///  Making an API call for getting Activity by Participant
    /// </summary>
    /// <param name="participantId"></param>
    /// <param name="onCompletion"></param>
    /// <param name="onError"></param>
    private void ActivityByParticipant(string participantId, Action<string> onCompletion,
          Action<RestUtil.RestCallError> onError)
    {
        var builder = new WebRequestBuilder()
            .Url(RestManager.FormatApiUrl(Urls.ACTIVITY_BY_PARTICIPANT, participantId))
            .Verb(Verbs.GET)
            .ContentType(ContentTypes.JSON);

      ///  RestManager.SendWebRequest(builder, onCompletion, onError);
    }

    /// <summary>
    ///  Making an API call for getting Activity by Price
    /// </summary>
    /// <param name="price"></param>
    /// <param name="onCompletion"></param>
    /// <param name="onError"></param>
    private void ActivityByPrice(string price, Action<string> onCompletion,
          Action<RestUtil.RestCallError> onError)
    {
        var builder = new WebRequestBuilder()
             .Url(RestManager.FormatApiUrl(Urls.ACTIVITY_BY_PRICE, price))
             .Verb(Verbs.GET)
             .ContentType(ContentTypes.JSON);

      //  RestManager.SendWebRequest(builder, onCompletion, onError);
    }

    /// <summary>
    ///  Making an API call for getting Activity by Price Range
    /// </summary>
    /// <param name="minPrice"></param>
    /// <param name="maxPrice"></param>
    /// <param name="onCompletion"></param>
    /// <param name="onError"></param>
    private void ActivityByPriceRange(string minPrice, string maxPrice, Action<string> onCompletion,
          Action<RestUtil.RestCallError> onError)
    {
        var builder = new WebRequestBuilder()
            .Url(RestManager.FormatApiUrl(Urls.ACTIVITY_BY_PRICE_RANGE, minPrice, maxPrice))
            .Verb(Verbs.GET)
            .ContentType(ContentTypes.JSON);

    //    RestManager.SendWebRequest(builder, onCompletion, onError);
    }

    /// <summary>
    ///  Making an API call for Registrating an user.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <param name="onCompletion"></param>
    /// <param name="onError"></param>
    private void PostRegistration(string username, string email, string password, Action<string> onCompletion,
         Action<RestUtil.RestCallError> onError)
    {
        var builder = new WebRequestBuilder()
            .Url(RestManager.FormatApiUrl(Urls.REGISTRATION))
            .Verb(Verbs.POST)
            .ContentType(ContentTypes.JSON)
            .FormData("Username", username)
            .FormData("Email", email)
            .FormData("Password", password);
        
//
      //  RestManager.SendWebRequest(builder, onCompletion, onError);
    }
    /// <summary>
    /// Get Login Details API
    /// </summary>
    /// <param name="token"></param>
    /// <param name="onCompletion"></param>
    /// <param name="onError"></param>
    private void GetLoginDetails(string token,Action<string> onCompletion,
          Action<RestUtil.RestCallError> onError)
    {
        var builder = new WebRequestBuilder()
            .Url(RestManager.FormatApiUrl(Urls.PROFILE_DETAILS))
            .Verb(Verbs.GET)
            .ContentType(ContentTypes.JSON);

      //  RestManager.AddClientAuthHeader(ref builder, token);
       // RestManager.SendWebRequest(builder, onCompletion, onError);
    }

    public void GetRandomActivity()
    {
        RandomActivity(Setdata, onErrorGetDetails);
    }

    public void GetActivityByKey()
    {
        ActivityByKey("5881028", Setdata, onErrorGetDetails);
    }

    public void GetActivityByType()
    {
        ActivityByType("recreational", Setdata, onErrorGetDetails);
    }

    public void GetActivityByParticipant()
    {
        ActivityByParticipant("2", Setdata, onErrorGetDetails);
    }

    public void GetActivityByPrice()
    {
        ActivityByPrice("0.4", Setdata, onErrorGetDetails);
    }
    public void GetActivityByPriceRange()
    {
        ActivityByPriceRange("0","5", Setdata, onErrorGetDetails);
    }
    private void Setdata(string datas)
    {
         Debug.Log("Caslled" + datas);
        jsonLogger.text = datas;// DataConverter.SerializeObject(datas);
    }    

    private void onErrorGetDetails(RestUtil.RestCallError restCallError)
    {
        Debug.LogError("RecivingUpdateUserInfo Error: " + restCallError.Description);
    }
}
[Serializable]
public class Root
{
    public string activity;//{ get; set; }
    public double accessibility;//{ get; set; }
    public string type;// { get; set; }
    public int participants;//{ get; set; }
    public double price;// { get; set; }
    public string key;// { get; set; }
}

