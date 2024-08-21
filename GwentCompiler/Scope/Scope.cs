using System;
namespace GwentCompiler
{
    public class Scope
    {
        Scope? parent;
        Dictionary <GwentVar , GwentObject> table;

        public Scope(Scope? Parent = null)
        {
            parent = Parent;
            table = new Dictionary<GwentVar, GwentObject>();
        }
        public void Set(GwentVar var , GwentObject value)
        {
            table[var] = value;                 //magia
        }

        public GwentObject Get(GwentVar var)
        {
            if(table.ContainsKey(var))
            {
                return table[var];
            }
            if(parent!= null)
            {
                return parent.Get(var);
            }
            throw new NullReferenceException ("La variable " + var + " no existe en el contexto actual.");
        }
    }

}
