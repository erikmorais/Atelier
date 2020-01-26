#Notes:#

Services: 

Tax Calculation Service

Order Totais Calculation Service : depende on Tax Services

#Important 1#:
A country can have many taxes. Each calculation can be distinct. 
Services are ready to be extended to apply different calculation rules for n taxes for each country
They way services were defined, they can be moved to different servers. 
For example order calculation services receives a order and returns a order, it permits this service being moved to another server where data can be serialized

#bug fixes: order data context#
There was a bug related to memory leak in the statics methods Create Order and return order.
Since it is a bug, this methods were fixed, otherwise, it could be extended instead.
The implementation is a purpose to fix it. It does not mean is should go to production, since other clients depends on it. 


Welcome to Atelier Entertainment!

We are in the process of creating our worldwide content ordering system and we have lost our Lead Developer.

We asked them to work on the component they started that needs to do the following:

1. Create an Order
2. Retrieve a Single Order
3. Show all Orders by a Single Customer

In addition we asked them to implement the following rules:

* When an Order comes from Australia, add 10% Sales Tax
* When an Order comes from the UK, add 20% Sales Tax
* Each Order should calculate a Total for all items in that Order

We share our data context with another part of the system, so please do not change the exposed method signatures or access modifiers.

Please address the following according to your own best practices.

Best regards

The Atelier Entertainment Team

