import { BookNav } from './../../catalog/models/book-nav';
import { Book } from './../../catalog/models/book';
import { Injectable } from '@angular/core';
import { Http, RequestOptions } from "@angular/http";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/publishLast';
 
const dev_base_address = "https://localhost:44345/api/";
const deployment_base_address = "https://bookstoreartsheva.azurewebsites.net/api/";

@Injectable()
export class BooksService { 
  readonly base_address: String;
  
  list$: Observable<Book[]>;
  dictionary: Observable<Object>;

  constructor(private http: Http) {
    this.base_address =  deployment_base_address;
    // keep in cache the last result  
    this.list$ = this.loadBooks().publishLast().refCount();
    
    
  }

  fetchBooks(): Observable<Book[]> { // Array<Book>
    return this.list$;
  }

  fetchDictionary(): Observable<Object> {
    
    return this.list$.map(item => 
      item.reduce(function(map, obj) {
          map[obj.id] = obj;
          return map;
      }, {})
    );
  }

  getBook(bookId: string): Observable<BookNav> {
    // return this.http.get(url+bookId+apiKey).map(response => response.json());
    return this.fetchBooks().map(books => {

      const book = books.filter(b => b.id === bookId)[0];
      const index = books.indexOf(book);
      const count = books.length;
      const previousId = index > 0 ? books[index - 1].id : null;
      const nextId = index < count - 1 ? books[index + 1].id : null;
      return { book, previousId, nextId, index, count };
    });
  }

  loadBooks(): Observable<Book[]> { 
    
    return this.http.get(this.base_address + "books").map(data => data.json())
                    .map(obj => Object.values(obj));

  }


}
