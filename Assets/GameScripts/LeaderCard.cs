namespace GwentPro
{
    public sealed class LeaderCard : CardClass
    {
        void Start ()
        {
            base.Awake(); // Call the Start method of the base class
                          // Additional start logic specific to LeaderCard

            cmbtype = combatype.Leader;
            newcardHeight = 0.242236167f;
            newCardLength = 0.164927125f;
            ResizeCardObj();
        }
    }
}