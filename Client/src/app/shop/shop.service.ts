import { Injectable, OnInit } from '@angular/core';
import { Product } from '../shared/models/product';
import { HttpClient, HttpParams } from '@angular/common/http';
import { pagination } from '../shared/models/pagination';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';
import { ShopParams } from '../shared/models/ShopParams';


@Injectable({
  providedIn: 'root'
})
export class ShopService  {

  baseUrl="http://localhost:5143/api/";
  constructor(private http: HttpClient) { }
  getProducts(ShopParams:ShopParams){
    let params = new HttpParams();
    if(ShopParams.brandId) params = params.append('brandId',ShopParams.brandId);
    if(ShopParams.typeId) params = params.append('typeId',ShopParams.typeId);
     params =params.append('sort',ShopParams.sort);
     params =params.append('PageIndex',ShopParams.pageNumber);
     params =params.append('PageSize',ShopParams.pageSize);
     if(ShopParams.search) params =params.append('search',ShopParams.search);
    return this.http.get<pagination<Product[]>>(this.baseUrl+'Products',{params:params});
  }
  getProduct(id:number){
    return this.http.get<Product>(this.baseUrl+'Products/'+id);
  }
  getBrands(){
    return this.http.get<Brand[]>(this.baseUrl+'Products/brands')
  }
  getTypes(){
    return this.http.get<Type[]>(this.baseUrl+'Products/types')
  }

  
 
}
