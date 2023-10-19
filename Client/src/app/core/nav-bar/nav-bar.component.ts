import { Component } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { basketItem } from 'src/app/shared/models/basket';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {

  constructor(public basketService:BasketService){}
  getCount(items:basketItem[]){
    return items.reduce((Sum , item) => Sum + item.quantity ,0 ) ;
  }
  
}
