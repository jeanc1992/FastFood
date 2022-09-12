import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductComponent } from './pages/product/product.component';
import { AddProductComponent } from './components/add-product/add-product.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ReactiveFormsModule } from '@angular/forms';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
@NgModule({
  declarations: [ProductComponent, AddProductComponent],
  imports: [
    CommonModule,
    ModalModule.forRoot(),
    ReactiveFormsModule,
    SweetAlert2Module
  ]
})
export class ProductsModule { }
