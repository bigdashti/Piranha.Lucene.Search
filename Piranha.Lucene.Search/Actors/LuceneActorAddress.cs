using Proto;

namespace Piranha.Lucene.Search.Actors
{
    public class LuceneActorAddress : ILuceneActorAddress
    {
        public LuceneActorAddress(ActorSystem actorSystem, PID pid)
        {
            ActorSystem = actorSystem;
            Pid = pid;
        }

        public ActorSystem ActorSystem { get; }
        public PID Pid { get; }
    }
}