/// <summary>
/// 
/// </summary>
public class Continuation
{
    public Continuation SubContinuation { get; set; }
    public int NextStep { get; set; }
    public object Param { get; set; }
}
/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class Context<T>
{
    public Continuation Continuation { get; set; }
    public T Self { get; set; }
}
/// <summary>
/// 
/// </summary>
/// <typeparam name="TCleverUnit"></typeparam>
/// <typeparam name="TResult"></typeparam>
public interface IState<TCleverUnit, TResult>
{
    TResult Drive(Context<TCleverUnit> ctx);
}
