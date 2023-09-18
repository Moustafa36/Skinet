import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Skinet';
  //when you use scrite mode or ts in angluar you must fiv it an intial value
  constructor(private http: HttpClient)
  {

  }
  ngOnInit(): void {
    
  }
}
