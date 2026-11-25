using Microsoft.AspNetCore.Mvc;
using RSSFeedReader.Api.Models;
using RSSFeedReader.Api.Services;

namespace RSSFeedReader.Api.Controllers;

[ApiController]
[Route("api/subscriptions")]
public sealed class SubscriptionsController(SubscriptionStore store) : ControllerBase
{
    [HttpGet]
    public ActionResult<IReadOnlyList<Subscription>> GetAll()
    {
        return Ok(store.GetAll());
    }

    [HttpPost]
    public ActionResult<Subscription> Add([FromBody] AddSubscriptionRequest request)
    {
        var subscription = store.Add(request.Url);
        if (subscription is null)
        {
            return BadRequest("A subscription URL is required.");
        }

        return Created($"/api/subscriptions", subscription);
    }
}
