namespace RentAMotto.Common.Abstraction.Pagination;

public class PagedResult<T>(long total, IEnumerable<T> items)
{
    /// <summary>
    /// Total de itens encontrados baseado nos parâmetros da pesquisa
    /// </summary>
    public long Total { get; set; } = total;

    /// <summary>
    /// Total de itens retornados nesta consulta baseado na paginação
    /// </summary>
    public int Count { get; set; } = items.Count();

    /// <summary>
    /// Itens
    /// </summary>
    public IEnumerable<T> Items { get; set; } = items;
}
