# Blackbird.io QuickBooks Online

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction

<!-- begin docs -->

QuickBooks is an accounting software package developed and marketed by Intuit. First introduced in 1992, QuickBooks products are geared mainly toward small and medium-sized businesses and offer on-premises accounting applications as well as cloud-based versions that accept business payments, manage and pay bills, and payroll functions.

## Before setting up 

Before you can connect you need to make sure that:

- You have a QuickBooks Online account
- You have an organization in QuickBooks Online and you know the organization ID

## Connecting

1. Navigate to Apps, and identify the QuickBooks Online app. You can use search to find it.
2. Click _Add Connection_.
3. Name your connection for future reference e.g. 'My QuickBooks Online connection'.
4. Fill in the 'API url' field, for example `https://sandbox-quickbooks.api.intuit.com`.
5. Fill in the 'Company ID' field with your QuickBooks Online organization ID.
6. Fill in the 'Minor version' field with the minor version of the QuickBooks Online API you want to use. For now, latest version is `70` (and it can start from `1`).
7. Click _Connect_.

## Actions

### Class

- **Get all classes**: Get all classes.
- **Get class by ID**: Get a class by ID.
- **Create class**: Create class, optionally with parent class reference.
- **Update class**: Update class, optionally with new name, active status, subclass status, sync token, domain, and fully qualified name.

### Customer

- **Get all customers**: Get all customers.
- **Get customer by ID**: Get a customer by ID.
- **Create customer**: Create a customer.
- **Update customer**: Update a customer.

### Invoice

- **Get all invoices**: Get all invoices.
- **Get invoice by ID**: Get an invoice by ID.
- **Create invoice**: Create an invoice with a single line item and a customer reference.
- **Update invoice**: Update an invoice with a new due date and class reference.
- **Delete invoice**: Delete an invoice.
- **Send invoice**: Send an invoice to billing email address or email provided in request.
- **Void invoice**: Void an invoice with a class reference and sync token.

### Payment

- **Get all payments**: Get all payments.
- **Get payment by ID**: Get a payment by ID.
- **Create payment**: Create a payment with a customer reference and invoice reference.
- **Update payment**: Update a payment with a new amount and class reference.
- **Delete payment**: Delete a payment.
- **Void payment**: Void a payment with a class reference and sync token.
- **Send payment**: Send payment to email address provided in request or billing email address.

### Vendor

- **Get all vendors**: Get all vendors.
- **Get vendor by ID**: Get a vendor by ID.
- **Update vendor**: Updates an existing vendor with provided details.
- **Create vendor**: Registers a new vendor with provided details.

## Events

### Class Events
- **On classes created:** This event is triggered when a class or classes are created.
- **On classes updated:** This event is triggered when a class or classes is updated.
- **On classes deleted:** This event is triggered when a class or classes is deleted.

### Vendor Events
- **On vendors created:** This event is triggered when a vendor or vendors are created.
- **On vendors updated:** This event is triggered when a vendor or vendors is updated.
- **On vendors merged:** This event is triggered when a vendor or vendors is merged.
- **On vendors deleted:** This event is triggered when a vendor or vendors is deleted.

### Customer Events
- **On customers created:** This event is triggered when a customer or customers are created.
- **On customers updated:** This event is triggered when a customer or customers is updated.
- **On customers merged:** This event is triggered when a customer or customers is merged.
- **On customers deleted:** This event is triggered when a customer or customers is deleted.

### Invoice Events
- **On invoices created:** This event is triggered when an invoice or invoices is created.
- **On invoices updated:** This event is triggered when an invoice or invoices is updated.
- **On invoices voided:** This event is triggered when an invoice or invoices is voided.
- **On invoices deleted:** This event is triggered when an invoice or invoices is deleted.

### Payment Events
- **On payments created:** This event is triggered when a payment or payments is created.
- **On payments updated:** This event is triggered when a payment or payments is updated.
- **On payments voided:** This event is triggered when a payment or payments is voided.
- **On payments deleted:** This event is triggered when a payment or payments is deleted.

## Feedback

Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.

<!-- end docs -->
