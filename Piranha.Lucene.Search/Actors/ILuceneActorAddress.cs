using Proto;

namespace Piranha.Lucene.Search.Actors
{
    public interface ILuceneActorAddress
    {
        ActorSystem ActorSystem { get; }
        PID Pid { get; }
    }
}