import { Component, ElementRef, ViewChild } from '@angular/core';
import { Injectable, OnInit } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';
import { ShopParams } from '../shared/models/ShopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent  implements OnInit {
  
  @ViewChild('Search')searchTerm?:ElementRef
  products: Product[] =[] ;
  brands:Brand[]=[];
  types:Type[]=[];
  shopParams = new ShopParams();
  sortOptions=[ 
     {name:"Alphabetical",value:"name"},
     {name:"Price: low to high",value:"priceAsc"},
     {name:"Price: high to low",value:"priceDesc"}
  ]
  totalCount = 0 ; 
  
  constructor(private shopservice : ShopService){}
  ngOnInit(): void {
   this.getProducts();
   this.getBrands();
   this.getTypes() ;
   
  }

  getProducts(){
    this.shopservice.getProducts(this.shopParams).subscribe({
      next: response =>{ this.products = response.data;
                         this.shopParams.pageNumber = response.pageIndex;
                         this.shopParams.pageSize = response.pageSize;
                         this.totalCount = response.count; 
                        },
      error: error => console.log(error)
    })
  }
  getBrands(){
    this.shopservice.getBrands().subscribe({
      next: response => this.brands = [{id:0,name:'All'},...response],
      error: error => console.log(error)
    })
  }
  getTypes(){
    this.shopservice.getTypes().subscribe({
      next: response => this.types = [{id:0,name:'All'},...response],
      error: error => console.log(error)
    })

  }

  onBrandSelected(brandId:number){
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }
  onTypeSelected(typeId:number){
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }
  onSortSelected(event:any){
    this.shopParams.sort = event.target.value;
    this.getProducts();
  }
  onPageChanged(event:any){
    if(this.shopParams.pageNumber!=event){
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }
  onSearch()
  {
    this.shopParams.search =this.searchTerm?.nativeElement.value;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }
  onRest()
  {
    if(this.searchTerm)this.searchTerm.nativeElement.value = '';
    this.shopParams =new ShopParams();
    this.getProducts(); 
  }

  

}
