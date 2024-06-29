using System;

namespace GwentPro
{
    public class SpecialCard : CardClass
    {
        void Start()
        {
            cmbtype = combatype.Leader;
            newcardHeight = 0.242236167f;
            newCardLength = 0.164927125f;
            ResizeCardObj();
        }

    }
}