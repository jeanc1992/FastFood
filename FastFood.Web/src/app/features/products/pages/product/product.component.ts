import { Component, OnDestroy, OnInit } from '@angular/core';
import { ProductResponseDto } from 'src/app/core/models/products/response/productResponseDto';
import { ProductsService } from 'src/app/core/services/products.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AddProductComponent } from '../../components/add-product/add-product.component';
import { Subscription } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit, OnDestroy {

  public products?: ProductResponseDto[];
  modalRef?: BsModalRef;
  subscription: Subscription | null = null;
  constructor(
    private productService: ProductsService,
    private modalService: BsModalService,
    private toastr: ToastrService) {
    this.getProducts();
  }
  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
  ngOnInit(): void {
  }

   
  openModalProduct(product:ProductResponseDto | null = null) {
    this.modalRef = this.modalService.show(AddProductComponent,{
      initialState:{
        product:product
      }
    });

   this.subscription = this.modalRef.content.onClose.subscribe({next:(value:boolean)=>{
       if(value)this.getProducts();
    }})
  }

  removeProduct(product:ProductResponseDto){
     this.productService.deleteProduct(product.id).subscribe({
      next:(res=>{
        this.toastr.success("Product removed");
        this.getProducts();
      })
     })
  }

  private getProducts(){
    this.productService.getAllProduct().subscribe({
      next:(resp)=>{
         if(resp.succeed)
          this.products = resp.result;
      }
    })
  }



}

