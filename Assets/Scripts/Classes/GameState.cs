public sealed class GameState {

    private static GameState instance = null;
    private static readonly object padlock = new object();
    private int educationLevel;
    //private int totalCollectedTrash;
    //private int totalRecycledPaper;
    //private int totalRecycledMetal;
    //private int totalRecycledPlastic;
    private int totalMoneyEarned;
    private int totalMoneySpent;

    GameState() {
        educationLevel = 0;
    }

    public static GameState GameStateInstance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new GameState();
                }
                return instance;
            }
        }
    }

    public void egresosBalance()
    {
    }
}
