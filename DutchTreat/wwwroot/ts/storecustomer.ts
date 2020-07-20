//export class StoreCustomer { //mozna exportowac i nie jest w global scope trzeba to w innych plikach importowac
class StoreCustomer {
    public visits:number = 0 ; // publczny member typu number
    public ourName: string;

    constructor(private firstName: string, private lastName:string) { //private przed zmiennymi tworzy te pola nie trzeba ich deklarować

    }

    public showName(name: string = " "): boolean { //zwraca boola i podstawiona domyslna wartosc
        alert(this.firstName + " " + this.lastName);
        return true;
    }

    set name(val) { // val jest zawsze
        this.ourName = val ; //this jest obowiązkowy - odwołanie do obecnego obiektu
    }
    get name() {
        return this.ourName;
    }
}
