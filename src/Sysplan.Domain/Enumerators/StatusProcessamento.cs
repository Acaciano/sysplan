using System;
using System.Collections.Generic;
using System.Text;

namespace Sysplan.Domain.Enumerators
{
    public enum StatusProcessamento
    {
        Pendente = 0,
        EmAndamento = 1,
        ConcluidoSucesso = 2,
        ConcluidoComErro = 3
    }
}
