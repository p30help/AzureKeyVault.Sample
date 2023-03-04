# AzureKeyVault.Sample

For getting ClientId, ClientSecret and TenantId:
- First go to **Azure Portal** -> **App Registrations** -> Click on **New registration** button 
- After define a new app you can find **Tenant Id** and **Client Id** of your app 
- Then go to **Certificates and Secrets** -> Click on **New clinet secret** -> After that you have an **Client secret** for you application
- Then go to your **KeyVault** portal and click on **Access configuration** -> Click on **Go to access policies** -> Click on **Create** button to see ***Create an access policy*** page 
-  On ***Pemission*** Tab check *Get, Set, List, Delete* items of **Secret Permission** section -> Click **Next**
- Then On **Principal** Tab select your App that defined on previous steps -> Then click on **Create** button to finalize your action