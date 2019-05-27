import { CartRow } from './../models/cart-row';
import { Book } from './../../catalog/models/book';
import { BooksService } from "./books.service";
import { Injectable } from '@angular/core'; 
import { HttpClient, HttpHeaders } from '@angular/common/http';

const dev_base_address = "https://localhost:44345/api/"; 

@Injectable()
export class CartService {

  rows: CartRow[] = []; 
  readonly base_address: String;
  
  readonly httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Access-Control-Allow-Origin': '*',
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicXdlcnR5IiwibmJmIjoxNTQ1OTQ1MzE2LCJleHAiOjE1NDYxMTgxMTYsImlzcyI6Ik15QXV0aFNlcnZlciIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTE4ODQvIn0.4wW20Um_AB6gnloqGnTuTULJGvr6FbbRxLzVsQ09nbc'
    })
  };
  constructor(private http: HttpClient, private library: BooksService) {
    // keep in cache the last result 
    this.base_address = dev_base_address;

    this.rows = new Array<CartRow>();
    this.http.get<Object[]>(this.base_address + "cart", this.httpOptions)
             .subscribe(response => {
                library.fetchDictionary().subscribe(dict => {
                    this.rows = response.map(
                      item => {
                        return new CartRow(dict[item["productId"]], item["quantity"]);
                      }
                    )
                }, error => {
                  console.log("Error happened while fetching books: " + error);
                })
              }, error => {
                console.log("Error happened while fetching cart contents: " + JSON.stringify(error));
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
    this.http.post(`${this.base_address}cart?item=${book.id}&quantity=${quantity}`, null, this.httpOptions)
             .subscribe(
                response => console.log("Response: "+ JSON.stringify(response)),
                error => console.log("Error: "+JSON.stringify(error))
             );  
  }

  remove(row: CartRow) {
    this.rows = this.rows.filter(r => r !== row);  
    this.http.delete(`${this.base_address}cart?item=${row.book.id}`, this.httpOptions)
             .subscribe(
               response => console.log("Response: " + response),
               error => console.log("Error: "+JSON.stringify(error))
            );
  }

  checkout() {
    this.http.post(`${this.base_address}cart/order`, null, this.httpOptions)
             .subscribe(
                response => alert("Success!"),
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
