using System.Collections.Generic;
using Newtonsoft.Json;
using System;

#if MIKROS_ADDED

using MikrosClient;

#endif

namespace EntertainmentFramework.Analytics.Enums
{
    public class PurchaseCategories
    {
        private enum PurchaseType
        {
            Currency,
            Character,
            CharacterSkin,
            Cosmetic,
            Weapon,
            Armor,
            LevelUnlock,
            ContentUnlock,
            TimeReduction,
            Bundle,
            Other
        }

        private readonly Dictionary<PurchaseType, List<string>> PurchaseTypeDict = new Dictionary<PurchaseType, List<string>>
        {
            {PurchaseType.Currency, new List<string>{"GOLD","SILVER","DIAMONDS","EMERALDS","GEMS","COINS","TOKENS","ENERGY","LIVES","BADGES"} },
            {PurchaseType.Character, new List<string>{ "Other" } },
            {PurchaseType.CharacterSkin, new List<string>{"SUPERHERO","FANTASY","SCI_FI","WESTERN","TRIBAL","CHRISTMAS","FESTIVAL","HALLOWEEN","EASTER","CELEBRITY",
                                                         "SCHOOL_UNIFORM","WASTELAND","NATURE","MILITARY","MACHINE","GOD","ANIMAL","FOOD"} },
            {PurchaseType.Cosmetic, new List<string>{"MAKEUP","AUDIO"} },
            {PurchaseType.Weapon, new List<string>{"SWORD","DAGGER","KNIFE","HAMMER","AXE","GUN","THROWABLE","MACE","STAFF"} },
            {PurchaseType.Armor, new List<string>{"SHIELD","HELMET","BODY_ARMOR","GAUNTLER","ACCESSORY"} },
            {PurchaseType.LevelUnlock, new List<string>{ "Other" } },
            {PurchaseType.ContentUnlock, new List<string>{ "Other" } },
            {PurchaseType.TimeReduction, new List<string>{ "Other" } },
            {PurchaseType.Bundle, new List<string>{ "Other" } },
            {PurchaseType.Other, new List<string>{ "Other" } }
        };

        public PurchaseCategories()
        { }

        public List<string> GetPurchaseCatagory(int purchaseType, int purchaseSubType)
        {
            string purchaseTYpe = ((PurchaseType)purchaseType).ToString();
            string purchaseSubTYpe = PurchaseTypeDict[(PurchaseType)purchaseType][purchaseSubType];
            List<string> returnData = new List<string>
            {
                purchaseTYpe,
                purchaseSubTYpe
            };
            return returnData;
        }
    }
}

[Serializable]
public class PurchaseDataInfo
{
    public PurchaseData purchaseData;
}

[Serializable]
public class PurchaseDetailsData
{
    public List<PurchaseInfo> purchaseDetails = new List<PurchaseInfo>();
    // public PurchaseData purchaseData;
}

public class PurchaseDetailInfo
{
    public PurchaseDetailsData purchaseData;
}

[Serializable]
public class PurchaseData
{
    public string skuName;

    public string skuDescription;

    public string skuType;

    public string skuSubType;

    public string purchaseType;

    public string purchaseCurrencyType;

    public float purchasePrice;

    public float percentDiscount;

    public float amountRewarded;

    public string uniquePurchaseId;
}

[Serializable]
public class PurchaseInfo
{
    public string skuName;

    public string skuDescription;

    public string skuType;

    public string skuSubType;

    public string timestamp;
}

public class SecondaryPurchaseInfo
{
    [JsonProperty(PropertyName = "subTypeSkuName")]
    public string skuName;

    [JsonProperty(PropertyName = "subSkuDescription")]
    public string skuDescription;

    [JsonProperty(PropertyName = "subSkuType")]
    public string skuType;

    [JsonProperty(PropertyName = "subSkuSubType")]
    public string skuSubType;

    [JsonProperty(PropertyName = "subTimestamp")]
    public string timestamp;
}

public class ScreenDetails
{
    public ScreeDataClass screenData;
}

[Serializable]
public class ScreeDataClass
{
    public string screenName;
    public string screenClass;
    public string timeSpentOnScreen;
}

[Serializable]
public class AchievementData
{
    public string achievementId;
    public string achievementName;
}

[Serializable]
public class AchievementDetails
{
    public List<AchievementData> achievementData;
}

public class ParticipantData
{
    public string userName;
    public string email;
    public PlayerBehavior participantBehavior;
}

public class ParticipantDatas
{
    public string userName;
    public string email;
    public string value;
}

public class PlayerRating
{
    public Sender sender;
    public List<ParticipantDatas> participants;
}

public class Sender
{
    public string deviceId;
    public string userName;
    public string email;
}

internal sealed class Exception
{
    public string detailMessage;
}