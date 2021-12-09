## Lab 5 - Password storage , Hashing

For this lab we used c# + asp.core for client-server webapp . Also we added Azure Cloud Provider for deploying wep-app 
, key-vault and db storage , in our case it was Azure DB(SQL Server). For hashing algorithm we used SHA-256 with salt.Also we store salt in a separate table.
For the security purpose we added password requirements for user like minimum 10 letters count.
Our database has next structure 

![alt text](../WebApplication/Properties/ReportImages/db-structure.jpg)

As you can see we have three tables : user , passwordSalt , IV(not interested in out case)

![alt text](../WebApplication/Properties/ReportImages/db-view.jpg)

For the lab purpose , we have created registration and login form , now  will registrate one user. 
As a result we got a hashed password(with salt in separate table) + sensitive data encryption(it`s for the next lab)

![alt text](../WebApplication/Properties/ReportImages/db-screenshot.jpg)

Next step we will login and check is all correct

![alt text](../WebApplication/Properties/ReportImages/login-form.jpg)
![alt text](../WebApplication/Properties/ReportImages/login-result.jpg)

## Lab 6 - Sensitive information storage

We've added the secret key with the help of which we encoded the sensitive user data with the AES algorithm. This key is contained not in database but in Azure Key Vault as a secret (data-k). 


![alt text](../WebApplication/Properties/ReportImages/secret-key.png)

With the help of this key and the fact that AES is symmetric algorithm, we've implemented encrypting the sensitive 
personal data in the database and decrypting this info while displaying it on the profile's user page.

We keep in database initialization vector for AES algorithm per each user in hex format.

## Lab 7 - TLS configuration

We've deployed our web app in Azure.
![alt text](../WebApplication/Properties/ReportImages/app-service.png)

We've decided to generate self-signed certificate with the tool OpenSSL.
For this task we've generated the private key with the pass phrase.
![alt text](../WebApplication/Properties/ReportImages/private_key.png)

After that we've created the CSR (Certificate Signing Request) with the help of private key.
![alt text](../WebApplication/Properties/ReportImages/csr.png)

After that we've created the certificate with the private key and csr. 
To successfully upload self-signed certificate to Azure we should add the serverAuth in the config file.

![alt text](../WebApplication/Properties/ReportImages/certificate_generate.png)

After successful uploading our self-signed certificate to Azure we bind it to our custom domain

![alt text](../WebApplication/Properties/ReportImages/tks_binding.png)

And as a result we have connected certificate to our web app

![alt text](../WebApplication/Properties/ReportImages/demonstration.png)
