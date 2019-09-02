namespace LaYumbaDemo.Tests
{
    // Only for reference: These functions are part of the library
    public static class EitherExtensions
    {
        // Core functions of Either: Map, ForEach, and Bind

        // Map
        // public static Either<L, RR> Map<L, R, RR>
        //     (this Either<L, R> either, Func<R, RR> f)
        //     => either.Match<Either<L, RR>>(
        //         l => Left(l),
        //         r => Right(f(r)));

        // // ForEach
        // public static Either<L, Unit> ForEach<L, R>
        //     (this Either<L, R> either, Action<R> act)
        //     => Map(either, act.ToFunc());

        // // Bind
        // public static Either<L, RR> Bind<L, R, RR>
        //     (this Either<L, R> either, Func<R, Either<L, RR>> f)
        //     => either.Match(
        //         l => Left(l),
        //         r => f(r));
    }
}