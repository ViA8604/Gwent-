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
        string type;
        string name;
        string faction;
        IExpression power;
        List<string> range;
        string image;
        EffectCallExpression OnActivation;

        public CardExpression(string Type, string Name, string Faction, IExpression Power, List<string> Range, string Image, EffectCallExpression onActivation)
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
            OnActivation.CheckCall();
            CompilerUtils.CardExpressions[name] = this;
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
            System.Console.WriteLine(jsondir);
            int pw = Convert.ToInt32(power.Evaluate().value);
            GwentPro.CardData card = new GwentPro.CardData(name, pw, type, range, faction, image, OnActivation.effectname);

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