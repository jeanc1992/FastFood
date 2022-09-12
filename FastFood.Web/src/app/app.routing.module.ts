import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrdersComponent } from './features/orders/pages/orders/orders.component';
import { ProductComponent } from './features/products/pages/product/product.component';

const routes: Routes = [
  { path: '', component: OrdersComponent },
  { path: 'products', component: ProductComponent }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
