namespace CrapsLibrary
{
    interface IBet
    {
        void EvaluateBet(byte firstOutcome, byte secondOutcome);
        bool MeetsFirstWinningCondition(byte firstOutcome, byte secondOutcome);
        bool MeetsLosingCondition(byte firstOutcome, byte secondOutcome);
    }
}
