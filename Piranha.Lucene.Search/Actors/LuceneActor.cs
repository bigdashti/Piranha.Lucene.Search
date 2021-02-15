using System.Threading.Tasks;
using Piranha.Lucene.Search.Actors.Commands;
using Proto;

namespace Piranha.Lucene.Search.Actors
{
    public class LuceneActor : IActor
    {
        public async Task ReceiveAsync(IContext context)
        {
            var message = context.Message;
            if (message is ILuceneActorCommand command) command.Run();
        }
    }
}