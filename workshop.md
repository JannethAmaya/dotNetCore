# Introduction to ASP.NET Core
ASP.NET 5 is a new open-source and cross-platform framework for building modern cloud-based Web applications using .NET. 
We built it from the ground up to provide an optimized development framework for apps that are either deployed to the cloud 
or run on-premises. It consists of modular components with minimal overhead, so you retain flexibility while constructing your 
solutions. You can develop and run your ASP.NET 5 applications cross-platform on Windows, Mac and Linux. ASP.NET 5 is fully 
open source on GitHub.

For more information go to [ASP.NET site](http://docs.asp.net/en/latest/conceptual-overview/aspnet.html)
# Midleware

Let's see an axample of Middleware. Add the following piece of code to _Configure_ method on *Startup.cs* file

```c#
app.UseRuntimeInfoPage();
```

Now go to [http://localhost:5000/runtimeinfo](http://localhost:5000/runtimeinfo)

## Another example
Replace the code of the Configure Method with the following:
```c#
app.UseRuntimeInfoPage();

app.UseDeveloperExceptionPage();
        
app.Run(async (context) => 
{
    throw new Exception("shit");
    await context.Response.WriteAsync("Hello World!");
}
);
```
and browse to your localhost page to see the result
#Dependency Injection
Create a Folder called **Services** and add a file Named **Greeter.cs**
```c#
namespace SampleWebApp.Services
{
    interface IGreeter
    {
        public string GetGreeting();
    }
    
    public class Greeter : IGreeter
    {
        public string GetGreeting()
        {
            return "Hello Nearsoft!";
        }
    }
}
```
on **Startup.cs**
Add _IGreeter_ dependency as a parameter on _Configure_ method

Then let's add the following piece of code to _ConfigureServices_ method
```c#
services.AddSingleton<IGreeter, Greeter>();
```
and don't forget to add the proper using statement for services namespace at the usings section:
```c#
using SampleWebApp.Services;
```


Now we have to configure the dependency injection on **Startup.cs**

# MVC
Now let's add a more cool piece of middeware.

First add the following code to Startup.cs File

**ConfigureServices** method
```c#
add.Mvc();
```

**Configure** method
```c#
app.UseMvcWithDefaultRoute();
```
Now let's create a folder on the root of our application called **Controllers**,
and afterwards add a file called **HomeController.cs** and place the following code on it:

```c#
using System;
using Microsoft.AspNet.Mvc;

namespace SampleWebApp.Controllers
{
    public class HomeController
    {
        public string Index()
        {
            return "Hello from Home Controller";
        }
    }
 }
```
Right now we are leveraging MVC's default routing implementation, 
but let's add some custom routes:

Go to **Startup.cs** file and add the following method:
```c#
private void ConfigureRoutes(IRouteBuilder routeBuilder)
{
    routeBuilder.MapRoute("Default"
    ,"{controller=Home}/{action=Index}/{id?}");
}
```
Next, replace the call of _UseMvcWithDefaultRoute_ with the following:
```c#
app.UseMvc(ConfigureRoutes);
```
**TASK:**
Now try to use lambda expresion instead of calling _ConfigureRoute_ method

What if we add another controller? - Add a new file called **AboutController.cs**
place the following code on it.

```c#
using System;
using Microsoft.AspNet.Mvc;

namespace SampleWebApp.Controllers
{
    public class AboutController
    {
        public string Phone()
        {
            return "6141984758";
        }
        
        public string Office()
        {
            return "CUU";
        }
    }
 }
```
## Attribute Routes
let's add a route definition for the About Controller. Update the **AboutController.cs** 
as follows:
```c#
[Route("[Controller]")]
public class AboutController
{
    [Route("[action]")]
    public string Phone()
    {
        return "6141984758";
    }
    
    [Route("")]
    public string Office()
    {
        return "CUU";
    }
}
```

## Action Results
So far we've been returning plain strings let's turn that up a little bit

First we need to add a model, so Add a **Models** folder and a new file on it
called **Nearsoftian.cs**, afterwards place the following  code on it:
```c#
namespace SampleWebApp.Models
{
    public class Nearsoftian
    {
        public string Name{get;set;}
        public string Phone{get;set;}
    }
}
```

change **HomeController.cs** to look like this:
```c#
using System;
using Microsoft.AspNet.Mvc;
using SampleWebApp.Models;

namespace SampleWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var nearsoftian = new Nearsoftian { Name = "Gus", Phone = "1234444" };
            return new ObjectResult(nearsoftian);
        }
    }
}
```

Now let's add a controller called **NearsoftController.cs** and a model 
called **Nearsoft.cs**


_Nearsoft.cs_
```c#
using System.Collections.Generic;

namespace SampleWebApp.Models
{
    public class Nearsoft
    {
        public List<Nearsoftian> Nearsoftians { get; set; }

        public Nearsoft()
        {
            Nearsoftians = new List<Nearsoftian>()
            {
                new Nearsoftian { Name = "Gus", Phone = "1234444" }
            };
        }
    }
}
```
_NearsoftController.cs_
```c#
using System;
using Microsoft.AspNet.Mvc;
using SampleWebApp.Models;

namespace SampleWebApp.Controllers
{
    public class NearsoftController : Controller
    {
        public IActionResult Index()
        {
            var nearsoft = new Nearsoft();
            return View(nearsoft);
        }
    }
}
```
Now we need to add the view, so add a **Views** folder and create a nother one inside of it called
**Nearsoft**

Add a file called **Index.cshtml** with the following content.
```
@addTagHelper "*, Microsoft.AspNet.Mvc.TagHelpers"
<html>
    <head>
        <title>Nearsoft</title>  
    </head>
    <body>
        <h1>Welcome to Nearsoft</h1>
        <h2>List of Nearsoftians:</h2>
        <ul>
            @foreach(var nearsoftian in Model.Nearsoftians)
            {
                <li>@nearsoftian.Name, @nearsoftian.Phone</li>
            }
        </ul>
    </body>
</html>  
```

Browse thw site an see the result
