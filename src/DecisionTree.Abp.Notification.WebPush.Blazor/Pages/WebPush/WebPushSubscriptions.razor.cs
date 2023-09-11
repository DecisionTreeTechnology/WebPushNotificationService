using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Volo.Abp.BlazoriseUI.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;
using DecisionTree.Abp.Notification.WebPush.Permissions;

namespace DecisionTree.Abp.Notification.WebPush.Blazor.Pages.WebPush
{
    public partial class WebPushSubscriptions
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        private IReadOnlyList<WebPushSubscriptionDto> WebPushSubscriptionList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateWebPushSubscription { get; set; }
        private bool CanEditWebPushSubscription { get; set; }
        private bool CanDeleteWebPushSubscription { get; set; }
        private WebPushSubscriptionCreateDto NewWebPushSubscription { get; set; }
        private Validations NewWebPushSubscriptionValidations { get; set; } = new();
        private WebPushSubscriptionUpdateDto EditingWebPushSubscription { get; set; }
        private Validations EditingWebPushSubscriptionValidations { get; set; } = new();
        private Guid EditingWebPushSubscriptionId { get; set; }
        private Modal CreateWebPushSubscriptionModal { get; set; } = new();
        private Modal EditWebPushSubscriptionModal { get; set; } = new();
        private GetWebPushSubscriptionsInput Filter { get; set; }
        private DataGridEntityActionsColumn<WebPushSubscriptionDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "webPushSubscription-create-tab";
        protected string SelectedEditTab = "webPushSubscription-edit-tab";
        
        public WebPushSubscriptions()
        {
            NewWebPushSubscription = new WebPushSubscriptionCreateDto();
            EditingWebPushSubscription = new WebPushSubscriptionUpdateDto();
            Filter = new GetWebPushSubscriptionsInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            WebPushSubscriptionList = new List<WebPushSubscriptionDto>();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:WebPushSubscriptions"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);
            
            Toolbar.AddButton(L["NewWebPushSubscription"], async () =>
            {
                await OpenCreateWebPushSubscriptionModalAsync();
            }, IconName.Add, requiredPolicyName: WebPushPermissions.WebPushSubscriptions.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateWebPushSubscription = await AuthorizationService
                .IsGrantedAsync(WebPushPermissions.WebPushSubscriptions.Create);
            CanEditWebPushSubscription = await AuthorizationService
                            .IsGrantedAsync(WebPushPermissions.WebPushSubscriptions.Edit);
            CanDeleteWebPushSubscription = await AuthorizationService
                            .IsGrantedAsync(WebPushPermissions.WebPushSubscriptions.Delete);
        }

        private async Task GetWebPushSubscriptionsAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await WebPushSubscriptionsAppService.GetListAsync(Filter);
            WebPushSubscriptionList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetWebPushSubscriptionsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private  async Task DownloadAsExcelAsync()
        {
            var token = (await WebPushSubscriptionsAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("WebPush") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/web-push/web-push-subscriptions/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<WebPushSubscriptionDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetWebPushSubscriptionsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateWebPushSubscriptionModalAsync()
        {
            NewWebPushSubscription = new WebPushSubscriptionCreateDto{
                
                
            };
            await NewWebPushSubscriptionValidations.ClearAll();
            await CreateWebPushSubscriptionModal.Show();
        }

        private async Task CloseCreateWebPushSubscriptionModalAsync()
        {
            NewWebPushSubscription = new WebPushSubscriptionCreateDto{
                
                
            };
            await CreateWebPushSubscriptionModal.Hide();
        }

        private async Task OpenEditWebPushSubscriptionModalAsync(WebPushSubscriptionDto input)
        {
            var webPushSubscription = await WebPushSubscriptionsAppService.GetAsync(input.Id);
            
            EditingWebPushSubscriptionId = webPushSubscription.Id;
            EditingWebPushSubscription = ObjectMapper.Map<WebPushSubscriptionDto, WebPushSubscriptionUpdateDto>(webPushSubscription);
            await EditingWebPushSubscriptionValidations.ClearAll();
            await EditWebPushSubscriptionModal.Show();
        }

        private async Task DeleteWebPushSubscriptionAsync(WebPushSubscriptionDto input)
        {
            await WebPushSubscriptionsAppService.DeleteAsync(input.Id);
            await GetWebPushSubscriptionsAsync();
        }

        private async Task CreateWebPushSubscriptionAsync()
        {
            try
            {
                if (await NewWebPushSubscriptionValidations.ValidateAll() == false)
                {
                    return;
                }

                await WebPushSubscriptionsAppService.CreateAsync(NewWebPushSubscription);
                await GetWebPushSubscriptionsAsync();
                await CloseCreateWebPushSubscriptionModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditWebPushSubscriptionModalAsync()
        {
            await EditWebPushSubscriptionModal.Hide();
        }

        private async Task UpdateWebPushSubscriptionAsync()
        {
            try
            {
                if (await EditingWebPushSubscriptionValidations.ValidateAll() == false)
                {
                    return;
                }

                await WebPushSubscriptionsAppService.UpdateAsync(EditingWebPushSubscriptionId, EditingWebPushSubscription);
                await GetWebPushSubscriptionsAsync();
                await EditWebPushSubscriptionModal.Hide();                
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private void OnSelectedCreateTabChanged(string name)
        {
            SelectedCreateTab = name;
        }

        private void OnSelectedEditTabChanged(string name)
        {
            SelectedEditTab = name;
        }
        

    }
}
