namespace GwentPro
{
    public sealed class LeaderCard : CardClass
    {
        void Start()
        {
            base.Awake(); // Call the Start method of the base class
                          // Additional start logic specific to LeaderCard

            combatTypes.Add(new CombatTypeListItem(combatype.Leader));
            newcardHeight = 0.242236167f;
            newCardLength = 0.164927125f;

        }
        public override void OnMouseDown()
        {
            base.OnMouseDown();
            base.SetActiveCamera();
            if (cardGame.Compiler != null)
            {
                cardGame.Compiler.ActivateEffect(cardname);
                player.alreadyplayed = true;
            }
        }
    }
}