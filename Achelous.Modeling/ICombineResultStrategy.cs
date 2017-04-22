namespace Achelous.DomainModeling
{
    public interface ICombineResultStrategy
    {
        IResult CombineResults(params IResult[] results);
    }
}
