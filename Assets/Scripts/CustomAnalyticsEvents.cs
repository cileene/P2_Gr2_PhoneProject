public class MessageSent : Unity.Services.Analytics.Event
{
    public MessageSent() : base("messageSent")
    {
    }
    
    public string Conversation { set { SetParameter("conversation", value); } }
    public int CurrentChoiceIndex { set { SetParameter("currentChoiceIndex", value); } }
    public int ChoiceIndex { set { SetParameter("choiceIndex", value); } }
}


public class PhotoViewed : Unity.Services.Analytics.Event
{
    public PhotoViewed() : base("photoViewed")
    {
    }
    
    public int PhotoIndex { set { SetParameter("photoIndex", value); } }
}


public class PlayedBirdGame : Unity.Services.Analytics.Event
{
    public PlayedBirdGame() : base("playedBirdGame")
    {
    }
    
    public int HighScore { set { SetParameter("highScore", value); } }
}


public class PlayedDeathGame : Unity.Services.Analytics.Event
{
    public PlayedDeathGame() : base("playedDeathGame")
    {
    }

    public string PlayerName { set { SetParameter("playerName", value); } }
    public int PlayerAge { set { SetParameter("playerAge", value); } }
    public bool PlayerSmokes { set { SetParameter("playerSmokes", value); } }
    public int PlayerCigarettesPerDay { set { SetParameter("playerCigarettesPerDay", value); } }
    public bool PlayerDrinks { set { SetParameter("playerDrinks", value); } }
    public int PlayerDrinksPerWeek { set { SetParameter("playerDrinksPerWeek", value); } }
    public int PlayerExerciseSessionsPerWeek { set { SetParameter("playerExerciseSessionsPerWeek", value); } }
    public int PlayerDietRating { set { SetParameter("playerDietRating", value); } }
    public int PlayerSleepHours { set { SetParameter("playerSleepHours", value); } }
    public int PlayerRiskRating { set { SetParameter("playerRiskRating", value); } }
    public string PlayerLivingEnvironment { set { SetParameter("playerLivingEnvironment", value); } }
    public bool PlayerFamilyHistory { set { SetParameter("playerFamilyHistory", value); } }
    public int PlayerDeathYear { set { SetParameter("playerDeathYear", value); } }
}


public class SceneLoaded : Unity.Services.Analytics.Event
{
    public SceneLoaded(string sceneName) : base("sceneLoaded")
    {
    }
    
    public string SceneName { set { SetParameter("sceneName", value); } }
}

