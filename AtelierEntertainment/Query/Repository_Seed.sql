﻿
delete from OrderItems
delete from Orders ;
delete from Customer;
delete from  CountryTaxes;

insert into CountryTaxes(TaxId ,Country ,Percentual) values (1,'UK',0.20);
insert into CountryTaxes(TaxId ,Country ,Percentual) values (2,'AU',0.10);

insert into Customer( Id, Country ) values ( 1,'UK') ;
insert into Customer( Id, Country ) values ( 2,'UK') ;
insert into Customer( Id, Country ) values ( 3,'AU') ;
insert into Customer( Id, Country ) values ( 4,'AU') ;
insert into Customer( Id, Country ) values ( 5,'BR') ;
insert into Customer( Id, Country ) values ( 6,'BR') ;


insert into Orders (Id ,Total , CustomerId , TotalTax) values (1,10,5,0);
insert into OrderItems( Code ,Price ,Description ,OrderId ,Quantity ) values ('A', 10,'product A' ,1 ,1);

insert into Orders (Id ,Total , CustomerId , TotalTax) values (2,20,6,0);
insert into OrderItems( Code ,Price ,Description ,OrderId ,Quantity ) values ('B', 5,'product B' ,2 ,2);
insert into OrderItems( Code ,Price ,Description ,OrderId ,Quantity ) values ('C', 2,'product C' ,2 ,5);

insert into Orders (Id ,Total , CustomerId , TotalTax) values (3,10,5,0);
insert into OrderItems( Code ,Price ,Description ,OrderId ,Quantity ) values ('D', 10,'product D' ,3 ,1);








