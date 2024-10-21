using GymManagement.Domain.Common;
using GymManagement.Infrastructure.Common.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
namespace GymManagement.Infrastructure.Common.Middleware
{
    public class EventualConsistencyMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context, IPublisher publisher, GymManagementDbContext gymContext)
        {
            var transaction = await gymContext.Database.BeginTransactionAsync(IsolationLevel.Serializable);
            context.Response.OnCompleted(async () =>
            {
                Stopwatch sw = Stopwatch.StartNew();
                try
                {
                    if (context.Items.TryGetValue("DomainEventsQueue", out var value) && value is Queue<IDomainEvent> domainEventsQueue)
                    {

                        while (domainEventsQueue!.TryDequeue(out var domainEvent))
                        {
                            await publisher.Publish(domainEvent);
                        }

                    }

                    await transaction.CommitAsync();

                }
                catch (Exception e)
                {
                    // We can do any error handling we want such as sending an email to the client for example
                    StreamWriter writer = new StreamWriter("errors.txt");
                    await writer.WriteLineAsync(e.Message);
                    writer.Close();
                }
                finally
                {
                    await transaction.DisposeAsync();
                    sw.Stop();
                    StreamWriter writer = new StreamWriter("time.txt");
                    await writer.WriteLineAsync(sw.ElapsedMilliseconds.ToString());
                    writer.Close();
                }


            });

            await next(context);
        }
    }
}
