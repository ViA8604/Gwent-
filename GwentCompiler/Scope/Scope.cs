using System;
namespace GwentCompiler
{
    public class Scope
    {
        Scope? parent;
        Dictionary<string, GwentType> types;
        Dictionary<string, GwentObject> values;

        public Scope(Scope? Parent = null)
        {
            parent = Parent;
            types = [];
            values = [];
        }
        
        public void SetType(string name , GwentType type) 
        {
            types[name] = type;
        }

        public GwentType GetType(string name)
        {
            if(types.ContainsKey(name))
            {
                return types[name];
            }
            if(parent != null)
            {
                return parent.GetType(name);
            }
            return GwentType.GwentNull;
        }

        public void SetValue(string name , GwentObject obj)
        {
            values[name] = obj;    //Hacer la l'ogica del chequeo de tipos
        }

        public GwentObject GetValue(string name)
        {
            if(values.ContainsKey(name))
            {
                return values[name];
            }
            if(parent != null)
            {
                return parent.GetValue(name);
            }
            throw new NullReferenceException ("Variable " + name + " sin inicializar.");
        }
    }

}
