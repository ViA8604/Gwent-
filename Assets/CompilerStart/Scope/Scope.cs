using System;
using System.Collections.Generic;
namespace GwentCompiler
{
    public class Scope
    {
        Scope? parent;
        public Dictionary<string, GwentType> types;
        public Dictionary<string, GwentObject> values;

        public Scope(Scope? Parent = null)
        {
            parent = Parent;
            types = new Dictionary<string, GwentType>();
            values = new Dictionary<string, GwentObject>();
        }
        
        public void SetType(string name , GwentType type) 
        {
            types[name] = type ;
        }

        public GwentType GetType(string name)
        {
            Scope? location = FindTypes(name);
            
            if(location == null)
                return GwentType.GwentNull;
            else
                return location.types[name];
        }

        public void SetValue(string name , GwentObject obj)
        {
            Scope? location = FindTypes(name);
            
            if(location == null)  // esto nunca se va a  ejecutar pq siempre antes de SetValue se hace setType
                return ;
            
            if(location.types[name] != obj.type)
                throw new Exception($"*RunTime Error* : variable {name} with type {location.types[name]} cannot be reasigned with type {obj.type}");
            
            location.values[name] = obj ;

        }

        public GwentObject GetValue(string name)
        {
            Scope? location = FindValue(name);
            if(location == null)
                throw new Exception($"*RunTime Error* : variable {name} has not been initialized");
            else 
                return location.values[name];
        }
        public Scope? FindValue(string name)
        {
            if(this.values.ContainsKey(name))
                return this;
            
            else if(parent != null)
                return parent.FindValue(name);
            
            else
                return null ;
        }
        public Scope? FindTypes(string name)
        {
            if(this.types.ContainsKey(name))
                return this;
            
            else if(parent != null)
                return parent.FindTypes(name);
            
            else
                return null ;
        }
    }

}
