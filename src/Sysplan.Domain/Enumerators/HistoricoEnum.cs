using System.ComponentModel;

namespace Sysplan.Domain.Enumerators
{
    public enum EventHistoricoEnum
    {
        [Description("Criar")]
        Criar = 1,
        [Description("Editar")]
        Editar = 2,
        [Description("Inativar")]
        Inativar = 3,
        [Description("Ativar")]
        Ativar = 4,
        [Description("Associar")]
        Associar = 5,
        [Description("Excluir")]
        Excluir = 6,
    }

    public enum TipoHistoricoEnum
    {
        [Description("CategoriaDieta")]
        CategoriaDieta = 1,
        [Description("LocalProducao")]
        LocalProducao = 2,
        [Description("ImportacaoArquivo")]
        ImportacaoArquivo = 3,
        [Description("ItemDieta")]
        ItemDieta = 4,
        [Description("ItemAbreviacaoJejum")]
        ItemAbreviacaoJejum = 5,
        [Description("Recipiente")]
        Recipiente = 6,
        [Description("JustificativaDietaIndividualizada")]
        JustificativaDietaIndividualizada = 7,
        [Description("UnidadeMedida")]
        UnidadeMedida = 8,
        [Description("Nutriente")]
        Nutriente = 9
    }
}
