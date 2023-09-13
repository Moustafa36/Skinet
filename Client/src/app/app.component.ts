import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Product }  from '../app/models/product';
import { pagination } from './models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Skinet';
  products:Product[]=[]; //when you use scrite mode or ts in angluar you must fiv it an intial value
  constructor(private http: HttpClient)
  {

  }
  ngOnInit(): void {
    this.http.get<pagination<Product[]>>('http://localhost:5143/api/Products?pageSize=50').subscribe({
      next:(response) => this.products=response.data,
      error: error => console.log(error),//what to do if there is an error
      complete:()=>{
        console.log("request completed");
      }
    });
  }
}
