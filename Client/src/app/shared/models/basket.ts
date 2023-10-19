import * as cuid from "cuid"

export interface basketItem {
    id: number ;
    productName:string ;
    price: number  ;
    quantity: number ;
    pictureUrl: string ;
    brand: string ;
    type: string ;
  }
  export interface basket {
    id: string;
    items: basketItem[];
  }

  export class basket implements basket {
    id=cuid()
    items: basketItem[]=[]
  }

  export interface BasketTotal{
    Shipping:number;
    SubTotal:number;
    Total:number;
  } 