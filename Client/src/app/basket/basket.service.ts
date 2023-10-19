import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BasketTotal, basket, basketItem } from '../shared/models/basket';
import { HttpClient } from '@angular/common/http';
import { Product } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseUrl=environment.apiUrl;

  private basketSource = new BehaviorSubject<basket|null>(null);

  basketSource$= this.basketSource.asObservable();
  private basketTotalSource =new BehaviorSubject<BasketTotal|null>(null);
   basketTotalSource$ =this.basketTotalSource.asObservable();

  constructor(private http:HttpClient) { }

  getBasket(id:string){
    return this.http.get<basket>(this.baseUrl+'basket?id='+id).
    subscribe({
                next:basket=>{
                  this.basketSource.next(basket);
                  this.calculateTotal();
                }
                     });
     }

     setBasket(basket:basket){
    return this.http.post<basket>(this.baseUrl+'basket',basket).
    subscribe({
      next:basket=>{
        this.basketSource.next(basket);
        this.calculateTotal();
      }
    });
  }

  getCurrentBasket(){
    return this.basketSource.value;
  }
   
  addItemToBasket(item:(Product|basketItem),quantity=1)
  {
    if(this.isProduct(item))item=this.mapProductItemToBasketItem(item);
    // console.log(itemToAdd);
    
    const _basket = this.getCurrentBasket()??this.CreatBasket();
     _basket.items=this.addOrUpdate(_basket.items,item,quantity);
     this.setBasket(_basket);
  }

  addOrUpdate(items: basketItem[], itemToAdd: basketItem, quantity: number): basketItem[] {
    
    const item  = items.find(x => x.id === itemToAdd.id);

    if(item){item.quantity+=quantity;  
    }
    else {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }
    return items;

  }

  removeItemFromBasket(id:number,quantity=1){
    const basket = this.getCurrentBasket();
    if(!basket)return;
    const item = basket.items.find(x=>x.id===id);
    if(item){
      console.log(item);
      item.quantity-=quantity;
      if(item.quantity===0)basket.items= basket.items.filter(x=>x.id!==id);
      if(basket.items.length>0)this.setBasket(basket);
      else this.deleteBasket(basket);
    }
  }
  deleteBasket(basket: basket) {
    return this.http.delete(this.baseUrl+'basket?id='+basket.id).subscribe({
      next:()=>{
        this.basketSource.next(null);
        this.basketTotalSource.next(null);
        localStorage.removeItem('basket_id');
      }
    })
  }

  private CreatBasket(): basket  {
    const _basket = new basket();
    localStorage.setItem('basket_id',_basket.id);
    return _basket ;
  }

  private mapProductItemToBasketItem(item: Product):basketItem {
    return {
      id:item.id,
      productName:item.name,
      price:item.price,
      quantity:0,
      pictureUrl:item.pictureUrl,
      brand:item.productBrand,
      type:item.productType
    };
  }

  private calculateTotal(){
    const basket =  this.getCurrentBasket();
    if(!basket) return; 
    const Shipping = 0 ; 
    const  SubTotal = basket.items.reduce((a,b) => (b.price * b.quantity) + a , 0);
    const Total = SubTotal + Shipping ;
    this.basketTotalSource.next({Shipping,SubTotal,Total});
  }

  private isProduct(item:Product|basketItem):item is Product{
    return (item as Product).productBrand!==undefined; 
  }








}
