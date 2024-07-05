using System;

namespace GwentPro
{
    public class SpecialCard : CardClass
    {
        void Start()
        {   
            base.Awake(); // Call the Start method of the base class
            cmbtype = combatype.Special;
            newcardHeight = 0.242236167f;
            newCardLength = 0.164927125f;
            ResizeCardObj();
        }

    }
}