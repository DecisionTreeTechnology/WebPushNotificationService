using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace DecisionTree.Abp.Notification.WebPush.Pages;

public class IndexModel : WebPushPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
