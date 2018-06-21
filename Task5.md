# Task 5

Implement [Stripe](http://stripe.com) payment gateway and store talent in [AWS S3](https://aws.amazon.com/s3/).

## Requirements

Add Stripe. [Tutorial](https://docs.google.com/document/d/1sBDyVmLOHkDB5cHqbnG3mzuwwv6y5QSMWhifBxKxvvI/edit)

Store talents photo in AWS S3

Get AWS Educate Account

## AWS Deliverables

### Create AWS Account

![Created account](Pictures/Task5/Created-account.png)

### Store talents photo in AWS S3

![S3 Bucket](Pictures/Task5/S3-Bucket.png)

## Code

The code for Task 5 can be found in this [branch](https://github.com/francisyzy/CSCAssignment/tree/Task5)

### Dependencies Used

#### Stripe.net

Stripe.net is a sync/async .NET 4.5+ client, and a portable class library for the Stripe API.  (Official Library)

[Nuget package manager](https://www.nuget.org/packages/Stripe.net/)
``` Install-Package Stripe.net ```

## Testing

### Stripe

*[Link to code](https://github.com/francisyzy/CSCAssignment/commit/30156bd9b437c364a2576bdf11edd5533ce840a2)*

![Stripe](Pictures/Task5/Stripe.png)

### Stripe validation

*[Link to code](https://github.com/francisyzy/CSCAssignment/blob/Task5/CSCAssignment/Views/Stripe/Stripe.cshtml#L249-L281)*

![Postman-http](Pictures/Task5/Stripe-error.png)

### Stripe loading gif

Loading process in stripe is too fast for me to take a screenshot. The CSS loading spinner is in the link to code below.

*[Link to code](https://github.com/francisyzy/CSCAssignment/blob/Task5/CSCAssignment/Views/Stripe/Stripe.cshtml#L13-L114)*

