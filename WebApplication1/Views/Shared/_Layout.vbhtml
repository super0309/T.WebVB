<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - My ASP.NET Application</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
    <link href="~/Content/app/main.css" rel="stylesheet" />
</head>
<body>
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                @Html.ActionLink("TWeb For Testing", "Index", "Home", New With {.area = ""}, New With {.class = "navbar-brand"})
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    @*<li>@Html.ActionLink("Home", "Index", "Home")</li>*@
                    <li>@Html.ActionLink("Customer", "Index", "Customer")</li>
                    <li>@Html.ActionLink("Order", "Index", "Order")</li>
                    
                </ul>
            </div>
        </div>
    </div>
    <div class="container body-content">
        @RenderBody()
        <hr />
        <footer>
            <p>&copy; @DateTime.Now.Year - My ASP.NET Application</p>
        </footer>
    </div>
    <div id="frmProgessBar" class="ProgressBar"><div class="loader"></div></div>

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required:=False)


    <script src="@Url.Content("~/Scripts/app/TWeb.js")"></script>
    <script src="@Url.Content("~/Scripts/app/Protocol.js")"></script>
    <script src="@Url.Content("~/Scripts/app/Customer.js")"></script>
    <script src="@Url.Content("~/Scripts/app/Order.js")"></script>
    <script src="@Url.Content("~/Scripts/app/Custom.js")"></script>
    <script>

        jQuery(document).ready(function () {
            var protocol = new Protocol();
            //MetronicComponents.init();
            console.log(protocol);
            TWeb.init(protocol);
        });
    </script>



</body>
</html>
