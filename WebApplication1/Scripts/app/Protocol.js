function Protocol() {
    if (!window.location.origin) {
        window.location.origin = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '');
    }
    this.ResgiterController = {};
    this.Host = window.location.origin;
    this.Link = window.location.href;
    this.Items = window.location.href.split('/');
    this.Action = this.Items[4];
    this.LinkAction = this.Link.substr(this.Link.indexOf(this.Action), this.Link.length - this.Link.indexOf(this.Action));
    //this.Controller = this.Link.replace(this.LinkAction, "");
    this.Controller = join(this.Items, '/');

    while (this.Controller.lastIndexOf('/') == this.Controller.length - 1) {
        this.Controller = this.Controller.slice(0, this.Controller.length - 1);
    }

    this.controller_name = this.Controller.replace(this.Host, "");

    this.ResgiterController.LoadCustomer = function (obj) {
        return obj.Link.indexOf("Customer") >= 0;
    }

    this.ResgiterController.LoadOrder = function (obj) {
        return obj.Link.indexOf("Order") >= 0;
    }

}