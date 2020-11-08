# BikeProduction

## Table of contents

- [General info](#general-info)
- [Screenshots](#screenshots)
- [Technologies](#technologies)
- [Setup](#setup)

## General info

BikeProduction - E-Commerce System.

Swagger:
http://localhost:5000 Identity
http://localhost:5100 CustomerOrder
http://localhost:5101 Catalog
http://localhost:5102 Production
http://localhost:5103 Warehouse 
http://localhost:5104 Delivery
http://localhost:5105 Basket

## Screenshots

Main Page
![](./img/user/MainPage.png)

Basket
![](./img/user/BasketView.png)

Orders
![](./img/user/OrdersView.png)

Order Detail
![](./img/user/OrderDetail.png)

Products
![](./img/user/ProductsView.png)

Product Detail
![](./img/user/ProductDetailView.png)

Admin Orders
![](./img/admin/AdminOrders.png)

Admin Products
![](./img/admin/AdminProducts.png)

Admin Loading Places
![](./img/admin/AdminLoadingPlaces.png)

Admin Loading Place Detail
![](./img/admin/AdminLoadingPlaceDetail.png)

Admin Packs
![](./img/admin/AdminPacks.png)

Admin Pack Detail
![](./img/admin/AdminPackDetail.png)

Admin Parts
![](./img/admin/AdminParts.png)

Admin Production Queues
![](./img/admin/AdminProductionQueues.png)

## Technologies

- ASP.NET Core (API & MVC).
- SQL Server.
- Docker.
- Redis.
- RabbitMQ.

## Setup

Requirements:

- Docker

To run App open BikeProduction.sln with Visual Studio, right click on docker-compose Set as Startup Project and press F5.

Passwords:

- Admin:
  admin/Admin123

- User:
  user/User123