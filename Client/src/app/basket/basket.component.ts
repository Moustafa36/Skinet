import { Component } from '@angular/core';
import { BasketService } from './basket.service';
import { basketItem } from '../shared/models/basket';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent {

  constructor(public basketService:BasketService) {} 
  
  incrementQuantity(item:basketItem){this.basketService.addItemToBasket(item);}
  removeItem(id:number,quantity:number){this.basketService.removeItemFromBasket(id,quantity);}
}
