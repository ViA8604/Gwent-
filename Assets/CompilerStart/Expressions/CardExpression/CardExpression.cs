using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static GwentPro.CardData;
namespace GwentCompiler
{
    public class CardExpression : IExpression
    {
        IExpression type;
        IExpression name;
        IExpression faction;
        IExpression power;
        List<string> range;
        IExpression image;
        EffectCallExpression OnActivation;

        public CardExpression(IExpression Type, IExpression Name, IExpression Faction, IExpression Power, List<string> Range, IExpression Image, EffectCallExpression onActivation)
        {
            type = Type;
            name = Name;
            faction = Faction;
            power = Power;
            range = Range;
            image = Image;
            OnActivation = onActivation;
        }

        public bool CheckSemantic()
        {
            type.CheckSemantic();
            name.CheckSemantic();
            faction.CheckSemantic();
            power.CheckSemantic();
            image.CheckSemantic();

            if (power.ReturnType != GwentType.GwentNumber)
            {
                throw new Exception("Power property return type must be number");
            }
            
            if(type.ReturnType != GwentType.GwentString || name.ReturnType != GwentType.GwentString)
                throw new Exception("Type and name expressions must return string");
            
            if(faction.ReturnType != GwentType.GwentString || image.ReturnType != GwentType.GwentString)
                throw new Exception("Faction and Image expressions must return string");
            OnActivation.CheckSemantic();
            return true;
        }

        public GwentObject Evaluate()
        {
            OnActivation.CheckCall();
            CreateJson();
            return new GwentObject(0, GwentType.GwentVoid);
        }


        public void ActiveEffect()
        {
            OnActivation.Evaluate();
        }

        void CreateJson()
        {
            string jsondir = "Assets/GameScripts/" + CompilerUtils.tag + ".json";//Directory.GetCurrentDirectory() + "/" + faction + ".json";//Path.Combine("../GameScripts/", faction + ".json");
            
            int pw = Convert.ToInt32(power.Evaluate().value);
            string cardName = name.Evaluate().value.ToString();
            string cardType = type.Evaluate().value.ToString();
            string cardFaction = faction.Evaluate().value.ToString();
            string cardImage = image.Evaluate().value.ToString();
            string effName = OnActivation.effectname.Evaluate().value.ToString();

            GwentPro.CardData card = new GwentPro.CardData(cardName, pw, cardType, range, cardFaction, cardImage, effName);

            List<GwentPro.CardData> cards;
            if (File.Exists(jsondir))
            {
                string oldjson = File.ReadAllText(jsondir);
                cards = JsonConvert.DeserializeObject<List<GwentPro.CardData>>(oldjson) ?? new List<GwentPro.CardData>();
            }
            else
            {
                cards = new List<GwentPro.CardData>();
            }

            cards?.Add(card);

            string json = JsonConvert.SerializeObject(cards ?? new List<GwentPro.CardData> { card }, Formatting.Indented);
            File.WriteAllText(jsondir, json);
            CompilerUtils.CardExpressions[cardName] = this;
        }

        public GwentType ReturnType => GwentType.GwentVoid;
        public override string ToString()
        {
            string output = "CardExpression: \n";
            output += "Type: " + type + "\n";
            output += "Name: " + name + "\n";
            output += "Faction: " + faction + "\n";
            output += "Power: " + power + "\n";
            output += "Range: [";
            foreach (string s in range)
            {
                output += s + " ,";
            }
            output += "] \nEffect: " + OnActivation.ToString();
            return output;
        }
    }


}