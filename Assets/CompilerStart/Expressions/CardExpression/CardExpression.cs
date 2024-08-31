using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace GwentCompiler
{
    public class CardExpression : IExpression
    {
        string type;
        string name;
        string faction;
        IExpression power;
        List<string> range;
        EffectCallExpression OnActivation;

        public CardExpression(string Type, string Name, string Faction, IExpression Power, List<string> Range, EffectCallExpression onActivation)
        {
            type = Type;
            name = Name;
            faction = Faction;
            power = Power;
            range = Range;
            OnActivation = onActivation;
        }

        public bool CheckSemantic()
        {
            power.CheckSemantic();
            if (power.ReturnType != GwentType.GwentNumber)
            {
                throw new Exception("Power property return type must be number");
            }
            OnActivation.CheckSemantic();
            return true;
        }

        public GwentObject Evaluate()
        {
            OnActivation.Evaluate();
            CreateJson();
            return new GwentObject(0, GwentType.GwentVoid);
        }

        public GwentType ReturnType => GwentType.GwentVoid;

        void CreateJson()
        {
            string jsondir = "Assets/GameScripts/" + CompilerUtils.tag + ".json";//Directory.GetCurrentDirectory() + "/" + faction + ".json";//Path.Combine("../GameScripts/", faction + ".json");
            System.Console.WriteLine(jsondir);
            int pw = Convert.ToInt32(power.Evaluate().value);
            CardData card = new CardData(name, pw, type, range[0], faction, "DefaultImage", OnActivation.effectname);

            List<CardData> cards;
            if (File.Exists(jsondir))
            {
                string oldjson = File.ReadAllText(jsondir);
                cards = JsonConvert.DeserializeObject<List<CardData>>(oldjson) ?? new List<CardData>();
            }
            else
            {
                cards = new List<CardData>();
            }

            cards?.Add(card);

            string json = JsonConvert.SerializeObject(cards ?? new List<CardData> { card }, Formatting.Indented);
            File.WriteAllText(jsondir, json);
        }

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

    public class CardData
    {
        public string Name;
        public int Points;
        public string CardType;
        public string CombatType;
        public string Faction;

        public string Image;
        public string EffectName;

        public CardData(string name, int points, string cardType, string combatType, string faction, string image, string effectName)
        {
            Name = name;
            Points = points;
            CardType = cardType;
            CombatType = combatType;
            Faction = faction;
            Image = image;
            EffectName = effectName;
        }
    }
}