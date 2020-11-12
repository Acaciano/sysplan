using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sysplan.Domain.Enumerators
{
    public enum FileReaderType
    {
        [Description("")]
        Default,
        [Description("CategoriaDieta")]
        CategoriaDieta,
        [Description("ItemDieta")]
        ItemDieta,
        [Description("ItemAbreviacaoJejum")]
        ItemAbreviacaoJejum,
        [Description("LocalProducao")]
        LocalProducao,
        [Description("Recipiente")]
        Recipiente,
        [Description("UnidadeMedida")]
        UnidadeMedida
    }
}
