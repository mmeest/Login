<p align="center"><img src="https://user-images.githubusercontent.com/34022590/111371494-2cd65500-86a2-11eb-8479-f2241d6b6e51.jpg" width="300px"></p>

<h1 align="center">
    <strong>ASP.NET User login</strong>
</h1>
<h3 align="center">
    <p>ASP.NET Application with register/login users to interact with database</p>
</h3>

### Contents
- [Tools needed](#tools-needed)
- [Components](#components)
- [Database](#database)
- [Creating Application](#creating-application)
- [Authentication](#authentication)
- [Users Table](#users-table)
- [Membership](#membership)
- [Login View](#login-view)
- [Routing](#routing)
- [Signup](#signup)
- [Navigation](#navigation)
- [Hiding Password](#hiding-password)

### Tools needed
* Visual Studio           - https://visualstudio.microsoft.com/
* Microsoft SQL Server    - https://www.microsoft.com/en-us/sql-server/sql-server-downloads
* Entity Framework        - https://docs.microsoft.com/en-us/ef/

### Components
| Type | Name | Value |
|-|-|-|
| Model | ./Model1.edmx | Database connection |
| Model | ./Models/Membership.cs | |
| View | ./Views/Shared/_Layout.cshtml | Navigation bar |
| View | ./Views/Account/Login.cshtml | View for logging in |
| View | ./Views/Account/Signup.cshtml | View for signing up |
| Controller | ./Controllers/EmployeesController.cs | For retreieving employees from database |
| Controller | ./Controllers/AccountController.cs | For |
| Config | ./Web.config | For authentication settings |
| Config | ./App_Start/RouteConfig.cs | For routing |

### Database
For our app we create new database named 'Office' with table 'Employee' :

| Column Name | Data Type | 
|-|-|
| Id | nvarchar(50) |
| Designation | nvarchar(50) |
| Salary | int |

'Id' column is Primary Key and with Identity

### Creating Application
1. In Visual Studio we create new 'ASP.NET Web Application' with 'MVC'\

Right click on solution and add new item 'ADO.NET Entity Data Model' named 'Model1'\
with 'EF Designer from database' and New Connection: with 'Office' database and 'Employee' table\

2. Next we add new 'MVC 5 Controller with views, using Entity Framework' controller to 'Controllers'\
with Model class 'Employee' and Data context class 'OfficeEntities' and name it 'EmployeesController'

With it we have created a full CRUD operation methods to our db table 'Employees'

### Authentication
Available in *System.Web.Security* namespace\
To implement
* Set **mode** in webconfig
* Use **FormsAuthentication.SetAuthCookie** for login
* Use **FormsAuthentication.SignOut** for logout

Now we add new Empty Controller 'AccountController' for authentication

On 'Web.config' we add to **<system.web>** section **<authentication>** section
```
<authentication mode="Forms">
		<forms loginUrl="Account/login"></forms>
</authentication>
```

On 'EmployeesController' we declare authorisation
```
namespace Login.Controllers
{
    [Authorize]                     // Authorized access only
    public class EmployeesController : Controller
    {
```

### Users Table
We'll add 'User' table to our 'Office' database:

| Column Name | Data Type |
|-|-|
| Id | int |
| UserName | nvarchar(50) |
| Password | nvarchar(50) |

'Id' column is Primary Key and with Identity

On Visual Studio in 'Model1.edmx' we 'Update Model from Database' and select 'User' table.

### Membership
In Models folder we add new class 'Membership' with 3 properties
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Membership
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
```

### Login view
On 'AccountController' we right click on GET action Login() and add view 'Login' with 'Create' template and 'Membership' Model class

Next we add httppost action 'Login'
```
// GET: Account
public ActionResult Login()
{
    return View();
}
[HttpPost]
public ActionResult Login(Models.Membership model)
{
    using(var context = new OfficeEntities())
    {
        bool isValid = context.User.Any(x=>x.UserName == model.UserName && x.Password == model.Password);
        if(isValid)
        {
            FormsAuthentication.SetAuthCookie(model.UserName, false);       // Security check for login
            return RedirectToAction("Index", "Employees");
        }
        ModelState.AddModelError("","Invalid username and password");       // Error message wor invalid login
        return View();
    }
}
```

And add using
```
using Login.Models;
```

### Routing
On 'RouteConfig.cs' we route our application to 'Employees'
```
defaults: new { controller = "Employees", action = "Index", id = UrlParameter.Optional }
```

### Signup
In 'AccountController.cs' we add sign up methods
```
// GET: Account
public ActionResult Signup()
{
    return View();
}
[HttpPost]
public ActionResult Signup(User model)
{
    using(var context = new OfficeEntities())
    {
        context.User.Add(model);
        context.SaveChanges();
    }
    return RedirectToAction("Login");
}
```

And on GET method we add signup view 'Signup' with 'Create' Template and 'User' Model class

We change our view footer as follows
```
<input type="submit" value="Signup" class="btn btn-default" />
//
<div>
    @Html.ActionLink("Click to login", "Login")
</div>
```

And change our 'Login.cshtml' footer as follows
```
<input type="submit" value="Login" class="btn btn-default" />
//
<div>
    @Html.ActionLink("Click to signup", "Signup")
</div>
```

Now we can register new users

We add to 'AccountController.cs'
```
using System.Web.Security;
```

### Navigation
Next we modify our navigation bar '_Layout.cshtml' according to user login
```
<div class="navbar-collapse collapse">
    <ul class="nav navbar-nav">
        <li>@Html.ActionLink("Get list", "Index", "Employees")</li>
        <li>@Html.ActionLink("Create", "Create", "Employees")</li>
        @if (User.Identity.IsAuthenticated)
        {
            <li><a>Dear - @User.Identity.Name</a></li>
            <li>@Html.ActionLink("Logout", "Logout", "Account")</li>
        }
        else
        {
            <li>@Html.ActionLink("Login", "Login", "Account")</li>
        }
        <li>Login</li>
    </ul>
</div>
```

### Hiding Password
To display dots instead of password we modify our 'Login.cshtml' and 'Signup.cshtml' password line from 'EditorFor' to 'PasswordFor'
```
@Html.PasswordFor(model => model.Password, new { htmlAttributes = new { @class = "form-control" } })
```


<p align="center"><h1>HAPPY CODING !!! :)</h1></p>
