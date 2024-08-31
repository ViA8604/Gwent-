using System;
using System.Collections.Generic;
using System.IO;
namespace GwentCompiler
{
    public interface IExpression {
        GwentObject Evaluate();
        bool CheckSemantic();
        GwentType ReturnType{get;}

    }
}
