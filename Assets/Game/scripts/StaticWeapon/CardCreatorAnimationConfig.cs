public static class CardCreatorAnimationConfig
{
    public static float GetXOffset(int countCards, float totalWidht)
    {
        switch (countCards)
        {
            case 3:
                return totalWidht / 3f;
            case 2:
                return totalWidht / 4f;
            case 1:
                return 0f;
            default:
                return totalWidht / 3f;
        }
    }
}