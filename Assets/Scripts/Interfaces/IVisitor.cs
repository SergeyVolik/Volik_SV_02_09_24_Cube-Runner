namespace CubeRunner
{
    public interface IVisitor<T> where T : class
    {
        void Visit(T toVisit);
    }
}