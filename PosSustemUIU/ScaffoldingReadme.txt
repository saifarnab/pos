Support for ASP.NET Core Identity was added to your project
- The code for adding Identity to your project was generated under Areas/Identity.

Configuration of the Identity related services can be found in the Areas/Identity/IdentityHostingStartup.cs file.


The generated UI requires support for static files. To add static files to your app:
1. Call app.UseStaticFiles() from your Configure method

To use ASP.NET Core Identity you also need to enable authentication. To authentication to your app:
1. Call app.UseAuthentication() from your Configure method (after static files)

The generated UI requires MVC. To add MVC to your app:
1. Call services.AddMvc() from your ConfigureServices method
2. Call app.UseMvc() from your Configure method (after authentication)

Apps that use ASP.NET Core Identity should also use HTTPS. To enable HTTPS see https://go.microsoft.com/fwlink/?linkid=848054.


<div class="row">
        <div class="col-md-4">
            <section>
                <form method="post">
                    <h4>Use a local account to log in.</h4>
                    <hr />
                    <div asp-validation-summary="All" class="text-danger"></div>
                    <div class="form-group">
                        <label asp-for="Input.Email"></label>
                        <input asp-for="Input.Email" class="form-control" />
                        <span asp-validation-for="Input.Email" class="text-danger"></span>
                    </div>
                    <div class="form-group">
                        <label asp-for="Input.Password"></label>
                        <input asp-for="Input.Password" class="form-control" />
                        <span asp-validation-for="Input.Password" class="text-danger"></span>
                    </div>
                    <div class="form-group">
                        <div class="checkbox">
                            <label asp-for="Input.RememberMe">
                                <input asp-for="Input.RememberMe" />
                                @Html.DisplayNameFor(m => m.Input.RememberMe)
                            </label>
                        </div>
                    </div>
                    <div class="form-group">
                        <button type="submit" class="btn btn-default">Log in</button>
                    </div>
                    <div class="form-group">
                        <p>
                            <a asp-page="./ForgotPassword">Forgot your password?</a>
                        </p>
                        <p>
                            <a asp-page="./Register" asp-route-returnUrl="@Model.ReturnUrl">Register as a new user</a>
                        </p>
                    </div>
                </form>
            </section>
        </div>
        <div class="col-md-6 col-md-offset-2">
            <section>
                <h4>Use another service to log in.</h4>
                <hr />
                @{
                    if ((Model.ExternalLogins?.Count ?? 0) == 0)
                    {
                        <div>
                            <p>
                                There are no external authentication services configured. See <a href="https://go.microsoft.com/fwlink/?LinkID=532715">this article</a>
                                for details on setting up this ASP.NET application to support logging in via external services.
                            </p>
                        </div>
                    }
                    else
                    {
                        <form asp-page="./ExternalLogin" asp-route-returnUrl="@Model.ReturnUrl" method="post" class="form-horizontal">
                            <div>
                                <p>
                                    @foreach (var provider in Model.ExternalLogins)
                                    {
                                        <button type="submit" class="btn btn-default" name="provider" value="@provider.Name" title="Log in using your @provider.DisplayName account">@provider.DisplayName</button>
                                    }
                                </p>
                            </div>
                        </form>
                    }
                }
            </section>
        </div>
    </div>
