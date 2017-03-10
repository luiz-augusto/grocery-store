# Grocery Store
# 1 - Installation
| Programs or frameworks | 
| ------ | 
| Visual Studio Community 2015 | 
| DotNet Framework 4.6.1 | 
| DotNetCore | 
| NodeJs | 

### 1.1 - Installing Visual Studio Community 2015

  - https://www.microsoft.com/en-us/download/details.aspx?id=48146

### 1.2 -Installing .NET Framework 4.6.1
  - Direct Link: https://www.microsoft.com/pt-br/download/details.aspx?id=49981

### 1.3 - Installing DotNetCore
  - https://www.microsoft.com/net/download/core#/sdk
  - Direct Link: ttps://go.microsoft.com/fwlink/?linkid=843448

### 1.4 - Installing Node-v6.9.1
  - x64 Direct Link: 
  https://nodejs.org/dist/v6.9.1/node-v6.9.1-x64.msi
  - x32 Direct Link: 
  https://nodejs.org/dist/v6.9.1/node-v6.9.1-x86.msi

After you install all programs, reboot your machine, open the prompt and type:
```
> node --version
> npm --version
```
It should return the version of the node and npm(this program is part of nodejs) that you have installed.

    After perform all the intallations, clone the repository, open the solution, wait the visual studio download the node_modules and perform a build in order to restore the nuget packages.

# 2 - Setup of the FrontEnd (Runtime)
	This step 2 is the easiest mode.

> If you want to run the frontend as developer mode, go to step 3.

Open the prompt and type:
```
> npm install http-server -g
```
Open the grocery-store-front folder and type:
```
> http-server ./dist 
```
You should see this output:
```
Starting up http-server, serving ./dist
Available on:
  http://192.168.25.4:8080
  http://127.0.0.1:8080
```
Copy one of the urls and paste in the browser.
Remember to run the GroceryStore.Api in order to load the data.
Right click in the GroceryStore.Api project and Set as Startup Project and then press F5.

# 3 - Setup of the FrontEnd (developer settings)
    If you want to run the project in developer mode.
    
Run the command
```
> npm install -g angular-cli
```
>It will install the tools required to run the frontend.
>You can use the Visual Studio 2015 ISSExpress to run this app too. Just do a right click in the file index.html, located in the root folder of the grocery-store-front and then select View in Brower.
>Using IISExpress, you will need run the API and GroceryStore.Api and after, on the browser, press F5 in the frontend page, in order to load the data from backend.
>Right click in the GroceryStore.Api project, set as Startup Project and press F5.

    The follows steps is to angular-cli, you can skip it, if you want to use the IISExpress.
Open the prompt, locate the project's folder and search the folder grocery-store-front. Open this folder and run the command:
```
> npm install
```
After install all the packages. Run this command:
```
> ng build
```
You should see this output:
```
chunk    {1} main.bundle.js, main.bundle.map (main) 33.1 kB {4} [initial] [rendered]
chunk    {2} styles.bundle.js, styles.bundle.map (styles) 13.3 kB {5} [initial] [rendered]
chunk    {3} scripts.bundle.js, scripts.bundle.map (scripts) 67 kB {5} [initial] [rendered]
chunk    {4} vendor.bundle.js, vendor.bundle.map (vendor) 2.72 MB [initial] [rendered]
chunk    {5} inline.bundle.js, inline.bundle.map (inline) 0 bytes [entry] [rendered]
```

Run the command:
```
> ng serve
```
Open the link: 
http://localhost:4200
Remember to run the GroceryStore.Api in order to load the data.
Right click in the GroceryStore.Api project, set as Startup Project and press F5.
If you have any problems with this port, you can open the file  GroceryStore.Api > Properties > lanchSettings.json and edit the port number. Open the file grocery-store-front > src > enviroment > (environment.ts|environment.prod.ts) and edit the port number too.

 Let me know if you have any problems!