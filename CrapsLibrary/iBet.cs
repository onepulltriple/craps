namespace CrapsLibrary
{
    interface IBet
    {
        void EvaluateBet(int firstOutcome, int secondOutcome);
        bool MeetsFirstWinningCondition(int firstOutcome, int secondOutcome);
        bool MeetsLosingCondition(int firstOutcome, int secondOutcome);
    }
}
