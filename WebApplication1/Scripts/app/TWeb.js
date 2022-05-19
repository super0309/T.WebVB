var TWeb =
{
    init: function (protocol) {
        this.loadPages(protocol);
    },
    loadPages: function (protocol) {

        if (protocol != null) {

            if (protocol.ResgiterController.LoadCustomer(protocol)) {
                Customer.init(protocol);
            }

            else if (protocol.ResgiterController.LoadOrder(protocol)) {
                Order.init(protocol);
            }
        }
    }
};


