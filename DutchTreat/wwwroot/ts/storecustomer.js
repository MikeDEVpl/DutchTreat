//export class StoreCustomer { //mozna exportowac i nie jest w global scope trzeba to w innych plikach importowac
var StoreCustomer = /** @class */ (function () {
    function StoreCustomer(firstName, lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.visits = 0; // publczny member typu number
    }
    StoreCustomer.prototype.showName = function (name) {
        if (name === void 0) { name = " "; }
        alert(this.firstName + " " + this.lastName);
        return true;
    };
    Object.defineProperty(StoreCustomer.prototype, "name", {
        get: function () {
            return this.ourName;
        },
        set: function (val) {
            this.ourName = val; //this jest obowiązkowy - odwołanie do obecnego obiektu
        },
        enumerable: false,
        configurable: true
    });
    return StoreCustomer;
}());
//# sourceMappingURL=storecustomer.js.map