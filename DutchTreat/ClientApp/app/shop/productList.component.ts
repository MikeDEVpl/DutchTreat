import { Component } from "@angular/core";

@Component({
    selector: "product-list",
    templateUrl: "productList.component.html",
    styleUrls:[]
})
export class ProductList {

    public products = [{
        title: "first product",
        price: 19.99
    }, {
            title: "second product",
            price: 9.99
        }, {
            title: "third product",
            price: 2.99
        },
    ];


}