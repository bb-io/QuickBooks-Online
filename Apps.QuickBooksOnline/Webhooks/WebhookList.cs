using System.Net;
using Apps.QuickBooksOnline.Actions;
using Apps.QuickBooksOnline.Api.Models.Responses;
using Apps.QuickBooksOnline.Contracts;
using Apps.QuickBooksOnline.Models.Requests.Classes;
using Apps.QuickBooksOnline.Models.Requests.Invoices;
using Apps.QuickBooksOnline.Models.Requests.Payments;
using Apps.QuickBooksOnline.Models.Requests.Vendors;
using Apps.QuickBooksOnline.Models.Responses;
using Apps.QuickBooksOnline.Models.Responses.Classes;
using Apps.QuickBooksOnline.Models.Responses.Invoices;
using Apps.QuickBooksOnline.Models.Responses.Vendors;
using Apps.QuickBooksOnline.Webhooks.Handlers;
using Apps.QuickBooksOnline.Webhooks.Models;
using Apps.QuickBooksOnline.Webhooks.Models.Responses;
using Apps.QuickBooksOnline.Webhooks.Payloads;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using ClassResponse = Apps.QuickBooksOnline.Models.Responses.Classes.ClassResponse;

namespace Apps.QuickBooksOnline.Webhooks;

[WebhookList]
public class WebhookList(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    #region Class events

    [Webhook("On classes created", typeof(ClassWebhookHandler), Description = "This event is triggered when a class or classes is created.")]
    public async Task<WebhookResponse<ClassesResponse>> OnClassesCreated(WebhookRequest request)
    {
        return await HandleClassWebhook(request, Operations.Create);
    }
    
    [Webhook("On classes updated", typeof(ClassWebhookHandler), Description = "This event is triggered when a class or classes is updated.")]
    public async Task<WebhookResponse<ClassesResponse>> OnClassesUpdated(WebhookRequest request)
    {
        return await HandleClassWebhook(request, Operations.Update);
    }
    
    [Webhook("On classes deleted", typeof(ClassWebhookHandler), Description = "This event is triggered when a class or classes is deleted.")]
    public Task<WebhookResponse<OnClassesDeletedResponse>> OnClassesDeleted(WebhookRequest request)
    {
        var payload = JsonConvert.DeserializeObject<QuickbooksEvent>(request.Body.ToString())
                      ?? throw new Exception("Could not deserialize request body.");

        var entities = payload.EventNotification.SelectMany(x => x.DataChangeEvent.Entities)
            .Where(x => x.Operation == Operations.Delete)
            .ToList();
        
        if (entities.Count == 0) 
        {
            return Task.FromResult(new WebhookResponse<OnClassesDeletedResponse>
            {
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
                ReceivedWebhookRequestType = WebhookRequestType.Preflight
            });
        }

        return Task.FromResult(new WebhookResponse<OnClassesDeletedResponse>()
        {
            HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
            Result = new OnClassesDeletedResponse { ClassIds = entities.Select(x => x.Id).ToList() }
        });
    }

    #endregion

    #region Vendor events
    
    [Webhook("On vendors created", typeof(VendorWebhookHandler), Description = "This event is triggered when a vendor or vendors is created.")]
    public async Task<WebhookResponse<GetAllVendorsResponse>> OnVendorsCreated(WebhookRequest request)
    {
        return await HandleVendorWebhook(request, Operations.Create);
    }
    
    [Webhook("On vendors updated", typeof(VendorWebhookHandler), Description = "This event is triggered when a vendor or vendors is updated.")]
    public async Task<WebhookResponse<GetAllVendorsResponse>> OnVendorsUpdated(WebhookRequest request)
    {
        return await HandleVendorWebhook(request, Operations.Update);
    }
    
    [Webhook("On vendors merged", typeof(VendorWebhookHandler), Description = "This event is triggered when a vendor or vendors is merged.")]
    public async Task<WebhookResponse<GetAllVendorsResponse>> OnVendorsMerged(WebhookRequest request)
    {
        return await HandleVendorWebhook(request, Operations.Merge);
    }
    
    [Webhook("On vendors deleted", typeof(VendorWebhookHandler), Description = "This event is triggered when a vendor or vendors is deleted.")]
    public Task<WebhookResponse<OnVendorsDeletedResponse>> OnVendorsDeleted(WebhookRequest request)
    {
        var payload = JsonConvert.DeserializeObject<QuickbooksEvent>(request.Body.ToString())
                      ?? throw new Exception("Could not deserialize request body.");

        var entities = payload.EventNotification.SelectMany(x => x.DataChangeEvent.Entities)
            .Where(x => x.Operation == Operations.Delete)
            .ToList();
        
        if (entities.Count == 0) 
        {
            return Task.FromResult(new WebhookResponse<OnVendorsDeletedResponse>
            {
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
                ReceivedWebhookRequestType = WebhookRequestType.Preflight
            });
        }

        return Task.FromResult(new WebhookResponse<OnVendorsDeletedResponse>()
        {
            HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
            Result = new OnVendorsDeletedResponse { VendorIds = entities.Select(x => x.Id).ToList() }
        });
    }

    #endregion

    #region Customer events
    
    [Webhook("On customers created", typeof(CustomerWebhookHandler), Description = "This event is triggered when a customer or customers is created.")]
    public async Task<WebhookResponse<GetCustomersResponse>> OnCustomersCreated(WebhookRequest request)
    {
        return await HandleCustomerWebhook(request, Operations.Create);
    }
    
    [Webhook("On customers updated", typeof(CustomerWebhookHandler), Description = "This event is triggered when a customer or customers is updated.")]
    public async Task<WebhookResponse<GetCustomersResponse>> OnCustomersUpdated(WebhookRequest request)
    {
        return await HandleCustomerWebhook(request, Operations.Update);
    }
    
    [Webhook("On customers merged", typeof(CustomerWebhookHandler), Description = "This event is triggered when a customer or customers is merged.")]
    public async Task<WebhookResponse<GetCustomersResponse>> OnCustomersMerged(WebhookRequest request)
    {
        return await HandleCustomerWebhook(request, Operations.Merge);
    }
    
    [Webhook("On customers deleted", typeof(CustomerWebhookHandler), Description = "This event is triggered when a customer or customers is deleted.")]
    public Task<WebhookResponse<OnCustomersDeletedResponse>> OnCustomersDeleted(WebhookRequest request)
    {
        var payload = JsonConvert.DeserializeObject<QuickbooksEvent>(request.Body.ToString())
                      ?? throw new Exception("Could not deserialize request body.");

        var entities = payload.EventNotification.SelectMany(x => x.DataChangeEvent.Entities)
            .Where(x => x.Operation == Operations.Delete)
            .ToList();
        
        if (entities.Count == 0) 
        {
            return Task.FromResult(new WebhookResponse<OnCustomersDeletedResponse>
            {
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
                ReceivedWebhookRequestType = WebhookRequestType.Preflight
            });
        }

        return Task.FromResult(new WebhookResponse<OnCustomersDeletedResponse>()
        {
            HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
            Result = new OnCustomersDeletedResponse { CustomerIds = entities.Select(x => x.Id).ToList() }
        });
    }

    #endregion

    #region Invoice events
    
    [Webhook("On invoices created", typeof(InvoiceWebhookHandler), Description = "This event is triggered when an invoice or invoices is created.")]
    public async Task<WebhookResponse<GetAllInvoicesResponse>> OnInvoicesCreated(WebhookRequest request)
    {
        return await HandleInvoiceWebhook(request, Operations.Create);
    }
    
    [Webhook("On invoices updated", typeof(InvoiceWebhookHandler), Description = "This event is triggered when an invoice or invoices is updated.")]
    public async Task<WebhookResponse<GetAllInvoicesResponse>> OnInvoicesUpdated(WebhookRequest request)
    {
        return await HandleInvoiceWebhook(request, Operations.Update);
    }
    
    [Webhook("On invoices voided", typeof(InvoiceWebhookHandler), Description = "This event is triggered when an invoice or invoices is voided.")]
    public async Task<WebhookResponse<GetAllInvoicesResponse>> OnInvoicesVoided(WebhookRequest request)
    {
        return await HandleInvoiceWebhook(request, Operations.Void);
    }
    
    [Webhook("On invoices deleted", typeof(InvoiceWebhookHandler), Description = "This event is triggered when an invoice or invoices is deleted.")]
    public Task<WebhookResponse<OnInvoicesDeletedResponse>> OnInvoicesDeleted(WebhookRequest request)
    {
        var payload = JsonConvert.DeserializeObject<QuickbooksEvent>(request.Body.ToString())
                      ?? throw new Exception("Could not deserialize request body.");

        var entities = payload.EventNotification.SelectMany(x => x.DataChangeEvent.Entities)
            .Where(x => x.Operation == Operations.Delete)
            .ToList();
        
        if (entities.Count == 0) 
        {
            return Task.FromResult(new WebhookResponse<OnInvoicesDeletedResponse>
            {
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
                ReceivedWebhookRequestType = WebhookRequestType.Preflight
            });
        }

        return Task.FromResult(new WebhookResponse<OnInvoicesDeletedResponse>()
        {
            HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
            Result = new OnInvoicesDeletedResponse { InvoiceIds = entities.Select(x => x.Id).ToList() }
        });
    }

    #endregion

    #region Payment events
    
    [Webhook("On payments created", typeof(PaymentWebhookHandler), Description = "This event is triggered when a payment or payments is created.")]
    public async Task<WebhookResponse<PaymentsResponse>> OnPaymentsCreated(WebhookRequest request)
    {
        return await HandlePaymentWebhook(request, Operations.Create);
    }
    
    [Webhook("On payments updated", typeof(PaymentWebhookHandler), Description = "This event is triggered when a payment or payments is updated.")]
    public async Task<WebhookResponse<PaymentsResponse>> OnPaymentsUpdated(WebhookRequest request)
    {
        return await HandlePaymentWebhook(request, Operations.Update);
    }
    
    [Webhook("On payments voided", typeof(PaymentWebhookHandler), Description = "This event is triggered when a payment or payments is voided.")]
    public async Task<WebhookResponse<PaymentsResponse>> OnPaymentsVoided(WebhookRequest request)
    {
        return await HandlePaymentWebhook(request, Operations.Void);
    }
    
    [Webhook("On payments deleted", typeof(PaymentWebhookHandler), Description = "This event is triggered when a payment or payments is deleted.")]
    public Task<WebhookResponse<OnPaymentsDeletedResponse>> OnPaymentsDeleted(WebhookRequest request)
    {
        var payload = JsonConvert.DeserializeObject<QuickbooksEvent>(request.Body.ToString())
                      ?? throw new Exception("Could not deserialize request body.");

        var entities = payload.EventNotification.SelectMany(x => x.DataChangeEvent.Entities)
            .Where(x => x.Operation == Operations.Delete)
            .ToList();
        
        if (entities.Count == 0) 
        {
            return Task.FromResult(new WebhookResponse<OnPaymentsDeletedResponse>
            {
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
                ReceivedWebhookRequestType = WebhookRequestType.Preflight
            });
        }

        return Task.FromResult(new WebhookResponse<OnPaymentsDeletedResponse>()
        {
            HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
            Result = new OnPaymentsDeletedResponse { PaymentIds = entities.Select(x => x.Id).ToList() }
        });
    }

    #endregion
    
    #region Utils
    
    private async Task<WebhookResponse<PaymentsResponse>> HandlePaymentWebhook(WebhookRequest request, string operation)
    {
        var payload = JsonConvert.DeserializeObject<QuickbooksEvent>(request.Body.ToString())
                      ?? throw new Exception("Could not deserialize request body.");

        var entities = payload.EventNotification.SelectMany(x => x.DataChangeEvent.Entities)
            .Where(x => x.Operation == operation)
            .ToList();

        if (entities.Count == 0)
        {
            return new WebhookResponse<PaymentsResponse>
            {
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
                ReceivedWebhookRequestType = WebhookRequestType.Preflight
            };
        }

        var payments = new List<PaymentResponse>();
        var paymentActions = new PaymentActions(InvocationContext);

        foreach (var entity in entities)
        {
            var paymentResponse = await paymentActions.GetPayment(new PaymentRequest { PaymentId = entity.Id });
            payments.Add(paymentResponse);
        }

        return new WebhookResponse<PaymentsResponse>
        {
            HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
            Result = new PaymentsResponse { Payments = payments }
        };
    }
    
    private async Task<WebhookResponse<GetAllInvoicesResponse>> HandleInvoiceWebhook(WebhookRequest request, string operation)
    {
        var payload = JsonConvert.DeserializeObject<QuickbooksEvent>(request.Body.ToString())
                      ?? throw new Exception("Could not deserialize request body.");

        var entities = payload.EventNotification.SelectMany(x => x.DataChangeEvent.Entities)
            .Where(x => x.Operation == operation)
            .ToList();

        if (entities.Count == 0)
        {
            return new WebhookResponse<GetAllInvoicesResponse>
            {
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
                ReceivedWebhookRequestType = WebhookRequestType.Preflight
            };
        }

        var invoices = new List<GetInvoiceResponse>();
        var invoiceActions = new InvoiceActions(InvocationContext, null);

        foreach (var entity in entities)
        {
            var invoiceResponse = await invoiceActions.GetInvoice(new InvoiceRequest { InvoiceId = entity.Id });
            invoices.Add(invoiceResponse);
        }

        return new WebhookResponse<GetAllInvoicesResponse>
        {
            HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
            Result = new GetAllInvoicesResponse { Invoices = invoices }
        };
    }
    
    private async Task<WebhookResponse<GetCustomersResponse>> HandleCustomerWebhook(WebhookRequest request, string operation)
    {
        var payload = JsonConvert.DeserializeObject<QuickbooksEvent>(request.Body.ToString())
                      ?? throw new Exception("Could not deserialize request body.");

        var entities = payload.EventNotification.SelectMany(x => x.DataChangeEvent.Entities)
            .Where(x => x.Operation == operation)
            .ToList();

        if (entities.Count == 0)
        {
            return new WebhookResponse<GetCustomersResponse>
            {
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
                ReceivedWebhookRequestType = WebhookRequestType.Preflight
            };
        }

        var customers = new List<GetCustomerResponse>();
        var customerActions = new CustomerActions(InvocationContext);

        foreach (var entity in entities)
        {
            var customerResponse = await customerActions.GetCustomer(new CustomerRequest { CustomerId = entity.Id });
            customers.Add(customerResponse);
        }

        return new WebhookResponse<GetCustomersResponse>
        {
            HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
            Result = new GetCustomersResponse { Customers = customers }
        };
    }
    
    private async Task<WebhookResponse<GetAllVendorsResponse>> HandleVendorWebhook(WebhookRequest request, string operation)
    {
        var payload = JsonConvert.DeserializeObject<QuickbooksEvent>(request.Body.ToString())
                      ?? throw new Exception("Could not deserialize request body.");

        var entities = payload.EventNotification.SelectMany(x => x.DataChangeEvent.Entities)
            .Where(x => x.Operation == operation)
            .ToList();

        if (entities.Count == 0)
        {
            return new WebhookResponse<GetAllVendorsResponse>
            {
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
                ReceivedWebhookRequestType = WebhookRequestType.Preflight
            };
        }

        var vendors = new List<VendorResponse>();
        var vendorActions = new VendorActions(InvocationContext);

        foreach (var entity in entities)
        {
            var vendorResponse = await vendorActions.GetVendorById(new VendorRequest { VendorId = entity.Id });
            vendors.Add(vendorResponse);
        }

        return new WebhookResponse<GetAllVendorsResponse>
        {
            HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
            Result = new GetAllVendorsResponse { Vendors = vendors }
        };
    }
    
    private async Task<WebhookResponse<ClassesResponse>> HandleClassWebhook(WebhookRequest request, string operation)
    {
        var payload = JsonConvert.DeserializeObject<QuickbooksEvent>(request.Body.ToString())
                      ?? throw new Exception("Could not deserialize request body.");

        var entities = payload.EventNotification.SelectMany(x => x.DataChangeEvent.Entities)
            .Where(x => x.Operation == operation)
            .ToList();

        if (entities.Count == 0)
        {
            return new WebhookResponse<ClassesResponse>
            {
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
                ReceivedWebhookRequestType = WebhookRequestType.Preflight
            };
        }

        var classes = new List<ClassResponse>();
        var classActions = new ClassActions(InvocationContext);

        foreach (var entity in entities)
        {
            var classResponse = await classActions.GetClassById(new ClassRequest { ClassId = entity.Id });
            classes.Add(classResponse);
        }

        return new WebhookResponse<ClassesResponse>
        {
            HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
            Result = new ClassesResponse { Classes = classes }
        };
    }
    
    #endregion
}