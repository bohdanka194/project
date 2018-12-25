import { CartRow } from './../models/cart-row';
import { Book } from './../../catalog/models/book';
import { Injectable } from '@angular/core'; 
import { Http, RequestOptions } from "@angular/http";

@Injectable()
export class CartService {

  rows: CartRow[] = [];

  constructor(private http: Http) {
    // keep in cache the last result  
  }

  add(book: Book, quantity: number = 1) {
    this.rows.push(new CartRow(book, quantity));
    console.log("https://localhost:44345/api/cart?item="+book.id+"&quantity="+quantity);
    this.http.post("https://localhost:44345/api/cart?item="+book.id+"&quantity="+quantity, null);
  }

  remove(row: CartRow) {
    this.rows = this.rows.filter(r => r !== row);
    this.http.delete("https://localhost:44345/api/cart?item="+row.book.id, null);
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
