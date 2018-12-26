import { CartRow } from './../models/cart-row';
import { Book } from './../../catalog/models/book';
import { BooksService } from "./books.service";
import { Injectable } from '@angular/core'; 
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class CartService {

  rows: CartRow[] = []; 
  readonly httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicXdlcnR5IiwibmJmIjoxNTQ1NzcxODY3LCJleHAiOjE1NDU5NDQ2NjcsImlzcyI6Ik15QXV0aFNlcnZlciIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTE4ODQvIn0.zZ88x0P6EZ6-weKafY_cCQUFzBySd1BGaIqZyIAvqoM'
    })
  };
  constructor(private http: HttpClient, private library: BooksService) {
    // keep in cache the last result 
    console.log("MAKING A GET REQUEST T0 https://localhost:44345/api/cart"); 
    this.http.get<Object[]>("https://localhost:44345/api/cart", this.httpOptions)
             .subscribe(response => {
                library.fetchDictionary().subscribe(dict => {
                    this.rows = response.map(
                      item => {
                        console.log("ITEM: "+ JSON.stringify(item));
                        return new CartRow(dict[item["productId"]], item["quantity"])
                      }
                    )
                }, error => {
                  console.log("Error happened while fetching books: " + error);
                })
              }, error => {
                console.log("Error happened while fetching cart contents: " + error);
                this.rows = new Array<CartRow>();
              })
  }

  add(book: Book, quantity: number = 1) {
    const match = this.rows.find(item => item.book.id == book.id);
    if(undefined != match && match.amout() > 0) {
      match.quantity += quantity;
    }
    else {
      this.rows.push(new CartRow(book, quantity));
    }
    console.log(
      "MAKING POST REQUEST TO https://localhost:44345/api/cart?item="+book.id+"&quantity="+quantity
    );
    this.http.post("https://localhost:44345/api/cart?item="+book.id+"&quantity="+quantity, null, this.httpOptions)
             .subscribe(
                response => console.log("Response: "+response),
                error => console.log("Error: "+JSON.stringify(error))
             );  
  }

  remove(row: CartRow) {
    this.rows = this.rows.filter(r => r !== row); 
    console.log("MAKING DELETE REQUEST TO https://localhost:44345/api/cart?item="+row.book.id);
    this.http.delete("https://localhost:44345/api/cart?item="+row.book.id, this.httpOptions)
             .subscribe(
               response => console.log("Response: "+response),
               error => console.log("Error: "+JSON.stringify(error))
            );
  }

  checkout() {
    console.log("MAKING POST REQUEST TO https://localhost:44345/api/cart/order");
    this.http.post("https://localhost:44345/api/cart/order", null, this.httpOptions)
             .subscribe(
                response => console.log("Response: "+response),
                error => console.log("Error: "+JSON.stringify(error))
             );  
  }

  total() {
    return this.rows.reduce((total, row) => {
      return total + row.book.price * row.quantity;
    }, 0)
  }

  count() {
    return this.rows
      .map(row => row.quantity)
      .reduce((count, value) => {
        return count + value;
      }, 0)
  }


  isEmpty() {
    return this.rows.length === 0;
  }

  clear(){
    this.rows = [];
  }

  getCartRows(){
    return this.rows;
  }

}
