Lab 5 - Password storage , Hashing

For this lab we used c# + asp.core for client-server webapp . Also we added Azure Cloud Provider for deploying wep-app 
, key-vault and db storage , in our case it was Azure DB(SQL Server). For hashing algorithm we used SHA-256 with salt.Also we store salt in a separate table.
For the security purpose we added password requirements for user like minimum 10 letters count.
Our database has next structure 

![alt text](../WebApplication/Properties/ReportImages/db-structure.jpg)

As you can see we have three tables : user , passwordSalt , IV(not interested in out case)

![alt text](../WebApplication/Properties/ReportImages/db-view.jpg)

For the lab purpose , we have created registration and login form , now  will registrate one user . 
As a result we got a hashed password(with salt in separate table) + sensitive data encryption(it`s for the next lab)

![alt text](../WebApplication/Properties/ReportImages/db-screenshot.jpg)

Next step we will login and check is all correct

![alt text](../WebApplication/Properties/ReportImages/login-form.jpg)
![alt text](../WebApplication/Properties/ReportImages/login-result.jpg)
